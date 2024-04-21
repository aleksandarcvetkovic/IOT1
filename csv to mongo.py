import csv
from pymongo import MongoClient

client = MongoClient('mongodb://localhost:27017/')

db = client['sobadb']

collection = db['senzor']

csv_file_path = 'C:\\Users\\Aleksandar\\Desktop\\iot_telemetry_data.csv'

def import_data_from_csv(csv_file_path, collection):
    with open(csv_file_path, 'r',encoding='utf-8-sig') as file:
        reader = csv.DictReader(file)
        cnt = 0
        for row in reader:
            #print(row)
            cnt +=1
            data = {
                'timestamp': row['ts'],
                'device': row['device'],
                'carbonMonoxide': row['co'],
                'humidity': row['humidity'],
                'light': row['light'],
                'motion': row['motion'],
                'smoke': row['smoke'],
                'temp': row['temp']
            }
            if cnt == 5:
                break
            collection.insert_one(data)
    print("Data imported successfully from CSV to MongoDB!")

import_data_from_csv(csv_file_path, collection)