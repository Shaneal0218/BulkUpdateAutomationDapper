import apiServiceUrl from './apiServiceUrl';
import axios from 'axios';

let apiService = {
    getTableNames: () => { return axios.get(apiServiceUrl.getTableNamesUrl())},
    getColumnNames: (tname) => { return axios.get(apiServiceUrl.getColumnNamesUrl() + "/" + tname)},
    bulkUpdate: (table) => { return axios.post(apiServiceUrl.bulkUpdateUrl(), table)}
}

export default apiService