<template>
  <div class="container mt-5">
      <b-row class="mb-3">
          <b-col md="3">
              <label for="supplier-id">Table Name</label>
              <b-dropdown id="supplier-id" v-bind:text="selectedTable" class="scroll w-100">
                  <b-dropdown-item v-for="(table, index) in tableNames" :key="index" @click="pickTableName(table)">{{table}}</b-dropdown-item>
              </b-dropdown>
          </b-col>
      </b-row>
      <b-row class="mb-3">
          <b-col md="2" v-for="(cname, index) in columnNames" :key="index">
              <label for="reorder-level-id">{{cname}}</label>
              <b-form-input v-model="product[cname]" type="text" id="reorder-level-id"></b-form-input>
          </b-col>
      </b-row>
      <b-row class="mb-3">
          <b-col md="2" style="margin-top: 30px;">
            <b-button @click="bulkUpdate()">Save Product</b-button>
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
  name: "Products",
  data() {
    return {
      product: {},
      columnNames: [],
      tableNames: [],
      selectedTable: "Search Table Name",
    };
  },
  mounted() {
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
      apiService.getColumnNames(this.selectedTable).then(response => { this.columnNames = response.data })
    },
    bulkUpdate() {
      var model = this.createSendRequestObject();
      console.log(model);
      apiService.bulkUpdate(model).then(response => { console.log(response.data) })
    },
    createSendRequestObject() {
      var valuePairs = [];
      var keys = Object.keys(this.product);
      var values = Object.values(this.product);
      for(var i = 0; i < keys.length; i++)
      {
        var obj = {property: keys[i], value: values[i]}
        valuePairs.push(obj)
      }
      return valuePairs
    }
  }
};
</script>

