let url = "http://localhost:50598/api/";

let apiServiceUrl = {
    getTableNamesUrl: () => { return url + 'bulkUpdate/getTableNames'},
    getColumnNamesUrl: () => { return url + 'bulkUpdate/getColumnNames'},
    bulkUpdateUrl: () => { return url + 'bulkUpdate/bulkUpdate'}
}

export default apiServiceUrl
