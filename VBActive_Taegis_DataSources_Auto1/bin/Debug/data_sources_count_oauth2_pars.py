# DATA SOURCES - CONTAGEM DOS DATA SOURCES DE UM CLIENTE
#          VERSÃO SDK/OAuth2 PARA SER USADA NA PRODUÇÃO DENTRO DE PROGRAMAS VB COM PARÂMETROS
""" 
AUTOR: HERON JR
DATA:  20/09/23
ALT:   
US2: https://api.delta.taegis.secureworks.com
Tenant Id: 137287
Tenant Name: Netcenter - Production
client_id: SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg
client_secret: ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
 """
 
 # ======================= Variáveis de importação =======================
import logging
import os
import sys
import argparse

from taegis_sdk_python import GraphQLService
from taegis_sdk_python.authentication import get_token_by_oauth
from taegis_sdk_python._consts import TAEGIS_ENVIRONMENT_URLS
from taegis_sdk_python.services.events.types import (EventQueryOptions, EventQueryResults)
from taegis_sdk_python.services.collector.types import(DataSourceMetrics, GetDataSourceMetricsArguments)
# ========================================================================================

# ======================= Disable Deprecated Commands Messages =======================
logging.getLogger("taegis_sdk_python.utils").setLevel(logging.ERROR)
logging.getLogger("taegis_sdk_python.services").setLevel(logging.ERROR)
# ========================================================================================

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pClientName: Nome do cliente
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que criará o arquivo com as quantidades de data sources para cada cliente
parser = argparse.ArgumentParser()
parser.add_argument('pClientName')
parser.add_argument('pEnvironment')
parser.add_argument('pTenantId')
parser.add_argument('pClientId')
parser.add_argument('pClientSecret')
args = parser.parse_args()

# =================================== Client Credentials ===================================
# Tenant settings
client = args.pClientName
env = args.pEnvironment
tenant_id = args.pTenantId
client_id = args.pClientId
client_secret = args.pClientSecret

# set request_url according to environment
request_url = TAEGIS_ENVIRONMENT_URLS[env]

# get access_token using OAuth2 credentials
access_token = get_token_by_oauth(
    request_url=request_url,
    client_id=client_id,
    client_secret=client_secret
)

service = GraphQLService(
    tenant_id=tenant_id,
    environment=env
)
# =============================================================================================

# ======================= Controle de Datas =======================
import datetime
from datetime import timedelta
from datetime import date
import calendar

hoje = datetime.date.today()
dia_01_mes_passado = (hoje - timedelta(days=hoje.day)).replace(day=1)
mes_passado = hoje.month
ano_mes_passado = hoje.year
_, dia_ult_mes_passado = calendar.monthrange(ano_mes_passado, mes_passado)
dia_ult_mes_passado = date(ano_mes_passado, mes_passado, 1) + timedelta(days=-1)

# ======================= Query =======================
with service(access_token=access_token):
    options = EventQueryOptions(
        timestamp_ascending=True,
        page_size=1000,
        max_rows=100000,
        skip_cache=True,
        aggregation_off=False,
    )

    result2 = service.collector.query.get_data_source_metrics(
        GetDataSourceMetricsArguments()
    )

# ======================= Query - Resultado Impresso  =======================
    data_source_qtd_warning = 0
    data_source_qtd_healthy = 0
    data_source_qtd_nodata = 0
    data_source_string = ''

    for data_source in result2.metrics:
        print(f"Source ID: {data_source}")
        # print(f"Tipo: {type(data_source)}")
        data_source_string = str(data_source)

        if 'NODATA' in data_source_string:
            data_source_qtd_nodata += 1
        elif 'WARNING' in data_source_string:
            data_source_qtd_warning += 1
        elif 'HEALTHY' in data_source_string:
            data_source_qtd_healthy += 1

    # print("")
    # print(f"Cliente: {client}")
    # print(f"Total de data_sources: {data_source_qtd_healthy+data_source_qtd_nodata+data_source_qtd_warning}")
    # print(f"Total de Healthy: {data_source_qtd_healthy}")
    # print(f"Total de Warnings: {data_source_qtd_warning}")
    # print(f"Total de Nodata: {data_source_qtd_nodata}")

# ======================= Query - Resultado Gravado em Arquivo CSV ======================= LAYOUT ORIGINAL
    # arquivo = open('data_sources_count_' + tenant_id + '_' + 
    #                hoje.strftime('%Y%m%d') + '.csv', 'w')
    # arquivo.write(hoje.strftime('%Y%m%d') + ';' + 
    #               tenant_id + ';' +
    #               client + ';' +
    #               str(data_source_qtd_healthy) + ';' + 
    #               str(data_source_qtd_warning) + ';' + 
    #               str(data_source_qtd_nodata))
    # arquivo.close()

# ======================= Query - Resultado Gravado em Arquivo CSV ======================= LAYOUT NOVO - 20/09/23
    arquivo = open('data_sources_count_' + tenant_id + '_' + dia_01_mes_passado.strftime('%Y%m%d') + '_' + 
                   dia_ult_mes_passado.strftime('%Y%m%d') + '.csv', 'w')
    arquivo.write(dia_01_mes_passado.strftime('%Y%m%d') + ';' + dia_ult_mes_passado.strftime('%Y%m%d') + ';' + 
                  str(data_source_qtd_healthy) + ';' + 
                  str(data_source_qtd_warning) + ';' + 
                  str(data_source_qtd_nodata))
    arquivo.close()

