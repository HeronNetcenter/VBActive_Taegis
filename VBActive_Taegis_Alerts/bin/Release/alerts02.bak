# ALERTAS - GERAÇÃO DO ARQUIVO CSV COM OS ALERTAS DO DIA ANTERIOR
""" 
AUTOR: HERON JR
DATA:  31/01/23
ALT:   13/02/23-08/12/23
       27/12/24: INCLUSÃO DO CAMPO FIRST_RESOLVED_AT
       13/03/25: AMPLIAÇÃO DO PERÍODO DE COLETA - SUBSTITUI "yesterday" POR UM PERÍODO ANTERIOR DE 3 MESES first_day_last_month
       16/03/25: CONTINUAM OS TESTES COM CHATGPT-IA PARA PERÍODOS MENORES DO QUE 3 MESES
US2: https://api.delta.taegis.secureworks.com

client_name	tenant_id	client_id	                        client_secret
NETCENTER   137287	    SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg	ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
NORTEC	    138529	    p0redWh1UyYnJxmaL6YYoCbmHBuf4vLA	yc5vFC_RFsFQzaYk_uc9thToqBagQaLEPZ-Hf4JJMZF4NuReNY6--eDlD9SvZDf-
TECHNOS	    142779	    2rvSMDA40yFD5BlnN05qzrJQ6tw1xP1P	f9Hl-QbK3D6BOBcToyNGuq6lAVCzio85GszUnpp9lw98iRx-5_H5oOWQAaUkYA3G
"""
import json

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que calculará a data do dia seguinte a partir da data informada como parâmetro.
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
parser.add_argument('pFirstDayLastMonth')
args = parser.parse_args()

from datetime import date, datetime, timedelta
from dateutil.relativedelta import relativedelta
today = date.today()
FIRST_DAY = args.pFirstDayLastMonth

# Get the first and the last days of the previous month for the process
if FIRST_DAY == '1_a_15':
  first_day_last_month = datetime(today.year, today.month - 1, 1) if today.month > 1 else datetime(today.year - 1, 12, 1)
elif FIRST_DAY == '16_a_fim':
  first_day_last_month = datetime(today.year, today.month - 1, 15) if today.month > 1 else datetime(today.year - 1, 12, 15)
elif FIRST_DAY == '1_a_10':
  first_day_last_month = datetime(today.year, today.month - 1, 1) if today.month > 1 else datetime(today.year - 1, 12, 1)
elif FIRST_DAY == '11_a_20':
  first_day_last_month = datetime(today.year, today.month - 1, 10) if today.month > 1 else datetime(today.year - 1, 12, 10)
elif FIRST_DAY == '21_a_fim':
  first_day_last_month = datetime(today.year, today.month - 1, 20) if today.month > 1 else datetime(today.year - 1, 12, 20)
elif FIRST_DAY == 'Mes':
  first_day_last_month = datetime(today.year, today.month - 1, 1) if today.month > 1 else datetime(today.year - 1, 12, 1)

# Arquivo de saída CSV - Encoding para aceitar alfabetos estranhos como o cirílico
arquivo = open('alerts02_' + args.pTenantId + '_' + first_day_last_month.strftime('%Y%m%d') + '.csv', 'w', encoding="utf-8")

# Cabeçalho
#                   1           2                 3           4           5           6               7                   8                   9                                    10                         11                              12                    13                  14                 15          16        17        18              19        20         21 
arquivo.write("Alert_Num;Attack Technique Ids;Entities;Ent-Relationships;ID;Investigation Ids;Metadata-Confidence;Metadata-Created At;Metadata-Creator Detector Id;Metadata-Creator Detector Version;Metadata-Creator Rule Id;Metadata-Creator Rule Version;Metadata-Engine Name;Metadata-Severity;Metadata-Title;Sensor Types;Status;Suppressed;Suppressed Rules;Tactics;Metadata-First Resolved At")    

# Arquivo de leitura
with open('alerts01_' + args.pTenantId + '_' + first_day_last_month.strftime('%Y%m%d') + '.json', 'r') as json_str:
    data = json.load(json_str)
    
isNone = 0
notNone=0
i = 0
total = len(data['data']['alertsServiceSearch']['alerts']['list'])

while i <= (total - 1):
    ddata01 = data['data']['alertsServiceSearch']['alerts']['list'][i]['attack_technique_ids']  # COL 2 (CSVED FILE)
    ddata02 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['entities']  # COL 3
    ddata03 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['relationships']  # COL 4
    ddata04 = data['data']['alertsServiceSearch']['alerts']['list'][i]['id']  # COL 5
    ddata05 = data['data']['alertsServiceSearch']['alerts']['list'][i]['investigation_ids']  # COL 6
    ddata06 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['confidence']    # COL 7

    # Alterado em 27/12/24 para incluir o campo first_resolved_at que quando nulo deve ficar = zero
    try:
        ddata20 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['first_resolved_at']['seconds']  # COL 21
        if ddata20 is None:
            ddata20 = 0
    except (KeyError, TypeError, IndexError):
        ddata20 = 0

    ddata07 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['created_at']['seconds']  # COL 8
    ddata08 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['detector_id']  # COL 9
    ddata09 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['version']  # COL 10
    ddata10 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['rule_id']  # COL 11
    ddata11 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['version']  # COL 12
    ddata12 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['engine']['name']  # COL 13
    ddata13 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['severity']  # COL 14
    ddata14 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['title']  # COL 15
    ddata15 = data['data']['alertsServiceSearch']['alerts']['list'][i]['sensor_types']  # COL 16
    ddata16 = data['data']['alertsServiceSearch']['alerts']['list'][i]['status']  # COL 17
    ddata17 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppressed']  # COL 18
    ddata18 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppression_rules']  # COL 19

    # O campo enrichment_details é uma lista com n dicionários 'mitre_attack_info' e cada um com um dicionário {'tactics': <valor>, 'technique_id': <valor>}
    ddata19 = data['data']['alertsServiceSearch']['alerts']['list'][i]['enrichment_details']  # COL 20
    i += 1

    # ddata02 - entities - Este campo é uma lista
    for j in range(len(ddata02)):
        if ';' in ddata02[j]:
            ddata02[j] = ddata02[j].replace(';', '~')

    # ddata03 - relationships - Este campo é uma lista
    #   Cada subcampo da lista ddata03 é um dicionário
    if isinstance(ddata03, type(None)):
        isNone+=1
    elif type(ddata03) is list:
        j = 0
        notNone+=1

        for j in range(len(ddata03)):
            strDic = str(ddata03[j])
            if ';' in strDic:
                strDic = strDic.replace(';', '~')
                ddata03[j] = eval(strDic)

    ddata14 =  ddata14.replace(';', ',')

    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata01};{ddata02};{ddata03};{ddata04};{ddata05};{ddata06};{ddata07};{ddata08};{ddata09};{ddata10};{ddata11};{ddata12};{ddata13};{ddata14};{ddata15};{ddata16};{ddata17};{ddata18};{ddata19};{ddata20}")    # Linha

# print(f"\nTotal: {total}")
arquivo.close()