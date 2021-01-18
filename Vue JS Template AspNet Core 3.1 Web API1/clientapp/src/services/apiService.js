import apiServiceUrl from './apiServiceUrl';
import axios from 'axios';

let apiService = {
    getTableNames: () => { return axios.get(apiServiceUrl.getTableNamesUrl())},
    getColumnNames: (tname) => { return axios.get(apiServiceUrl.getColumnNamesUrl() + "/" + tname)},
    getData: (tname) => { return axios.get(apiServiceUrl.getDataUrl() + "/" + tname)},
    bulkUpdate: (table) => { return axios.post(apiServiceUrl.bulkUpdateUrl(), table)},
    rowUpdate: (table) => { return axios.post(apiServiceUrl.rowUpdateUrl(), table)}
}

export default apiService