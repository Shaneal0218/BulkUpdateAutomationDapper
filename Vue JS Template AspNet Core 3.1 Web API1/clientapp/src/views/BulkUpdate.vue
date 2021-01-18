<template>
  <div id="bulkUpdate" class="container mt-5">
      <b-row class="mb-3">
          <b-col md="3">
              <label for="supplier-id">Table Name</label>
              <b-dropdown id="supplier-id" v-bind:text="selectedTable" class="scroll w-100">
                  <b-dropdown-item v-for="(table, index) in tableNames" :key="index" @click="pickTableName(table)">{{table}}</b-dropdown-item>
              </b-dropdown>
          </b-col>
      </b-row>
      <b-row class="mb-3">
          <b-col md="2" v-for="(c, index) in columnNames" :key="index">
              <label for="reorder-level-id">{{c.columN_NAME}}</label>
              <b-form-input v-if="c.datA_TYPE == 'int'" v-model="product[c.columN_NAME]" type="number" id="reorder-level-id"></b-form-input>
              <b-form-input v-if="c.datA_TYPE == 'string'" v-model="product[c.columN_NAME]" type="text" :maxlength="c.characteR_MAXIMUM_LENGTH" id="reorder-level-id"></b-form-input>
              <b-form-input v-if="c.datA_TYPE == 'datetime'" v-model="product[c.columN_NAME]" type="date" id="reorder-level-id"></b-form-input>
              <b-form-checkbox v-if="c.datA_TYPE == 'bit'" v-model="product[c.columN_NAME]"></b-form-checkbox>
          </b-col>
      </b-row>
      <b-row class="mb-3">
          <b-col md="2" style="margin-top: 30px;">
            <b-button @click="bulkUpdate()">Save Product</b-button>
          </b-col>
      </b-row>
      <b-row>
        <b-col>
          <table data-toggle="table" class="table table-striped table-bordered table-responsive dtr-inline">
            <thead>
              <tr>
                <th v-for="(field, index) in fields" :key="index">{{field}}</th>
              </tr>
            </thead>
            <tbody >
              <tr v-for="(item, index) in data" :key="index">
                <template v-if="item[fields[0]] != editId" >
                  <td v-for="(field, index) in fields" :key="index">{{item[field]}}</td>
                  <td>
                    <b-icon class="ml-3 pointer" icon="pencil-fill" aria-hidden="true" @click="editRow(item)"></b-icon>
                  </td>
                </template>
              </tr>
               <tr v-for="(item, index) in data" :key="index">
                <template v-if="item[fields[0]] == editId" >
                  <td v-for="(field, index) in fields" :key="index"><input v-model="item[field]" /></td>
                  <td>
                    <b-icon class="ml-3 pointer" icon="check-circle-fill" aria-hidden="true" @click="rowUpdate(item)"></b-icon>
                    <b-icon class="ml-3 pointer" icon="backspace-fill" aria-hidden="true" @click="cancelRowUpdate(item)"></b-icon>
                  </td>
                </template>
              </tr>
            </tbody>
          </table>
        </b-col>
    </b-row>
  </div>
</template>

<style>
  .scroll .dropdown-menu {
    max-height: 500px;
    overflow-y: auto;
  }
  .pointer {
    cursor: pointer;
  }
</style>

<script>
import apiService from '../services/apiService.js'

export default {
  name: "BulkUpdate",
  data() {
    return {
      filter: "",
      perPage: 10,
      item: {},
      field: {},
      currentPage: 1,
      data: [],
      product: {},
      columnNames: [],
      tableNames: [],
      fields: [],
      rowCopy: {},
      editId: 0,
      selectedTable: "Search Table Name",
    };
  },
  computed: {
    rows() {
       return this.data.length;
    }
  },
  mounted() {
    console.log(this.editId)
    apiService.getTableNames().then(response => { this.tableNames = response.data});
  },
  methods: {
    pickTableName (tname) {
      this.product = {};
      this.selectedTable = tname;
      this.product.table = tname;
      this.getColumnNames();
    },
    getColumnNames() {
      apiService.getColumnNames(this.selectedTable).then(response => { 
        this.columnNames = response.data;
        this.fields = [];
        for(var i = 0; i < this.columnNames.length; i++)
          {
            this.fields.push(this.columnNames[i].columN_NAME)
          } 
        })
      apiService.getData(this.selectedTable).then(response => { this.data = response.data })
    },
    editRow(row) {
      this.rowCopy = Object.assign({}, row);
      this.editId = row[this.fields[0]]
    },
    bulkUpdate() {
      var model = this.createSendRequestObject(this.product);
      console.log(model);
      apiService.bulkUpdate(model).then(response => 
      { 
        console.log(response.data)
        this.getColumnNames();
      })
    },
    rowUpdate(row) {
      var model = this.createSendRequestObject(row);
      model.unshift({property: "table", value: this.selectedTable})
      console.log(model)
      apiService.rowUpdate(model).then(response => 
      { 
        console.log(response.data)
        this.editId = 0;
        this.getColumnNames();
      })
    },
    cancelRowUpdate(row) {
      this.editId = 0;
      var oldRow = this.data.find(x => x[this.fields[0]] == row[this.fields[0]])
      Object.assign(oldRow, this.rowCopy);
    },
    createSendRequestObject(item) {
      var valuePairs = [];
      var keys = Object.keys(item);
      var values = Object.values(item);
      for(var i = 0; i < keys.length; i++)
      {
        var obj = {property: keys[i], value: values[i].toString()}
        valuePairs.push(obj)
      }
      return valuePairs
    }
  }
};
</script>

