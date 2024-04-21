
var PROTO_PATH =__dirname +'\\senzor.proto';

var grpc = require('@grpc/grpc-js');
var protoLoader = require('@grpc/proto-loader');
var packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {keepCase: true,
     longs: String,
     enums: String,
     defaults: true,
     oneofs: true
    });
var senzorsoba = grpc.loadPackageDefinition(packageDefinition).senzor;



function GetPodaci(call, callback) {
    var idSenzora = call.request.idSenzora;
    console.log("Primljen zahtev za " + idSenzora);
    if (idSenzora =="") {
      callback(new Error('Invalid idSenzora'), null);
    } else {
      findDocumentByAttributeValue('senzor', 'device', idSenzora).then((document) => {
          if (document) {
              callback(null, document);
          } else {
              callback(new Error('Senzor not found'), null);
          }
      });
    }
}

function PutPodaci(call, callback) {
    console.log(call.request.device);
    // Simulate an error
    if (call.request.device < 0) {
        callback(new Error('Invalid idSenzora'), null);
    } else {
        insertDocument('senzor', call.request);
        callback(null, {uspesno: 1, poruka: 'dodat je senzor'+ call.request.device});
    }
}
function DeletePodaci(call, callback) {
  var idSenzora = call.request.idSenzora;
  console.log("Primljen zahtev za brisanje " + idSenzora);
  if (idSenzora == "") {
    callback(new Error('Invalid idSenzora'), null);
  } else {
    deleteDocuments('senzor', 'device', idSenzora).then((result) => {
      if (result.deletedCount > 0) {
        callback(null, { uspesno: 1, poruka: 'Senzor successfully deleted, count: ' +  result.deletedCount});
      } else {
        callback(new Error('Senzor not found'), null);
      }
    });
  }
}
function UpdatePodaci(call, callback) {
  var idSenzora = call.request.device;
 
  console.log("Primljen zahtev za ažuriranje " + idSenzora);
  if (idSenzora == "") {
    callback(new Error('Invalid idSenzora'), null);
  } else {
    updateDocument('senzor', 'device', idSenzora, call.request).then((result) => {
      if (result.modifiedCount > 0) {
        callback(null, { uspesno: 1, poruka: 'Senzor successfully updated, count: ' +  result.modifiedCount});
      } else {
        callback(new Error('Senzor not found'), null);
      }
    });
  }
}

// MongoDB
const { MongoClient } = require('mongodb');
const uri = 'mongodb://mongoumrezi:27017';
var db;
const client = new MongoClient(uri);

async function connectToMongoDB() {
  try {
      await client.connect();
      db = await client.db('sobadb');
      console.log('Connected to MongoDB');
  } catch (error) {
      console.error('Error connecting to MongoDB:', error);
  }
}
async function insertDocument(collectionName, document) {
  try {
      const collection = db.collection(collectionName);
      const result = await collection.insertOne(document);
      console.log('Document inserted:', result.insertedId);
  } catch (error) {
      console.error('Error inserting document:', error);
  }
}
async function deleteDocuments(collectionName, attribute, value) {
  try {
    const collection = db.collection(collectionName);
    //delete all insted of one
    const result = await collection.deleteMany({ [attribute]: value });
    //const result = await collection.deleteOne({ [attribute]: value });
    console.log('Document deleted in mongo:', result.deletedCount);
    return result;
  } catch (error) {
    console.error('Error deleting document:', error);
  }
}

async function findDocumentByAttributeValue(collectionName, attribute, value) {
  try {
    const collection = db.collection(collectionName);
    const document = await collection.findOne({ [attribute]: value });
    //collection.find({ attribute: 'value' }).toArray(function(err, docs) {
      console.log('Document found in mongo:', document);
      return document;
    //});
  } catch (error) {
    console.error('Error finding document:', error);
  }
}
async function updateDocument(collectionName, attribute, value, updatedDocument) {
  try {
    const collection = db.collection(collectionName);
    const result = await collection.updateOne({ [attribute]: value }, { $set: updatedDocument });
    console.log('Document updated in mongo:', result.modifiedCount);
    return result;
  } catch (error) {
    console.error('Error updating document:', error);
  }
}

async function closeConnection() {
  try {
      await client.close();
      console.log('Disconnected from MongoDB');
  } catch (error) {
      console.error('Error closing connection:', error);
  }
}

process.on('SIGINT', () => {
  closeConnection().then(() => process.exit(0));
});

async function main() {
  var server = new grpc.Server();
  
  server.addService(senzorsoba.SenzorSoba.service, {GetPodaci: GetPodaci, PutPodaci: PutPodaci, DeletePodaci: DeletePodaci, UpdatePodaci: UpdatePodaci});
  server.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), () => {
  console.log('Server running at 0.0.0.0:50051');
  });
  await connectToMongoDB();
   //await insertDocument('customers', { name: 'John', age: 30 });
   
   //var doc  = await findDocumentByAttributeValue("customers", 'name', 'John');
   //console.log(doc);
   //await insertDocument('senzor',  { name: 'Jane', age: 35 });

//deleteDocuments('customers', 'name', 'John');
//updateDocument('senzor', 'device', "90:0f:00:70:91:0a", {de : "90:0f:00:70:91:0a", Temp : -25.5})
  //await findDocumentByAttributeValue('senzor', 'device', "00:0f:00:70:91:0a");

  //await updateDocument('customers', 'name', 'John', { name: 'Jane', age: 35 });
}

main();
