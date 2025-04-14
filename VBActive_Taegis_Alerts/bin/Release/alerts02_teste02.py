# ALERTAS - GERAÇÃO DO ARQUIVO CSV COM OS ALERTAS DO DIA ANTERIOR
""" 
AUTOR: HERON JR
DATA:  31/01/23
ALT:   13/02/23
 """
import json

pTenantId = '142779'
from datetime import datetime, timedelta
yesterday = datetime.now() - timedelta(days=1)

# Arquivo de saída CSV
arquivo = open('alerts02_' + pTenantId + '_' + yesterday.strftime('%Y%m%d') + '.csv', 'w', encoding="utf-8")
arquivo.write("Alert_Num;Attack Technique Ids;Entities;Ent-Relationships;ID;Investigation Ids;Metadata-Confidence;Metadata-Created At;Metadata-Creator Detector Id;Metadata-Creator Detector Version;Metadata-Creator Rule Id;Metadata-Creator Rule Version;Metadata-Engine Name;Metadata-Severity;Metadata-Title;Sensor Types;Status;Suppressed;Suppressed Rules;Tactics;Technique_id")    # Cabeçalho

# Arquivo de leitura
with open('alerts_500d_142779_20220729.json', 'r') as json_str:
    data = json.load(json_str)
    
i = 0
total = len(data['data']['alertsServiceSearch']['alerts']['list'])
# total = 2

while i <= (total - 1):
    ddata01 = data['data']['alertsServiceSearch']['alerts']['list'][i]['attack_technique_ids']
    ddata02 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['entities']
    ddata03 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['relationships']
    ddata04 = data['data']['alertsServiceSearch']['alerts']['list'][i]['id']
    ddata05 = data['data']['alertsServiceSearch']['alerts']['list'][i]['investigation_ids']
    ddata06 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['confidence']
    ddata07 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['created_at']['seconds']
    ddata08 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['detector_id']
    ddata09 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['version']
    ddata10 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['rule_id']
    ddata11 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['version']
    ddata12 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['engine']['name']
    ddata13 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['severity']
    ddata14 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['title']
    ddata15 = data['data']['alertsServiceSearch']['alerts']['list'][i]['sensor_types']
    ddata16 = data['data']['alertsServiceSearch']['alerts']['list'][i]['status']
    ddata17 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppressed']
    ddata18 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppression_rules']
    # O campo enrichment_details é uma lista com n dicionários 'mitre_attack_info' e cada um com um dicionário {'tactics': <valor>, 'technique_id': <valor>}
    ddata19 = data['data']['alertsServiceSearch']['alerts']['list'][i]['enrichment_details']

    i += 1
    ddata14 =  ddata14.replace(';', ',')

    print(f"Alert Num: {i}, \nattack_technique_ids: {ddata01}, \nEntities/Entities: {ddata02}, \nEntities/Relationships: {ddata03}, \nEntities/Id: {ddata04}, \nInvestigation_Ids: {ddata05}, \nMetadata/Confidence: {ddata06}, \nMetadata/Created at: {ddata07}, \nMetadata/Creator/Detector/Detector_Id: {ddata08}, \nMetadata/Creator/Detector/Version: {ddata09}, \nMetadata/Creator/Rule/Rule_Id: {ddata10}, \nMetadata/Creator/Rule/Version: {ddata11}, \nMetadata/Enginee/Name: {ddata12}, \nMetadata/Severity: {ddata13}, \nMetadata/Title: {ddata14}, \nSensor Types: {ddata15}, \nStatus: {ddata16}, \nSuppressed: {ddata17}, \nSuppression Rules: {ddata18}, \nEnrichment Details {ddata19}, \n====================================================================================================================================================================================================================")

    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata01};{ddata02};{ddata03};{ddata04};{ddata05};{ddata06};{ddata07};{ddata08};{ddata09};{ddata10};{ddata11};{ddata12};{ddata13};{ddata14};{ddata15};{ddata16};{ddata17};{ddata18};{ddata19}")    # Linha

print(f"\nTotal: {total}")
arquivo.close()