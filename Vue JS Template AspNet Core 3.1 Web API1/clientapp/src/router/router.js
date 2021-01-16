import Vue from 'vue'
import Router from 'vue-router'

const Container = () => import('@/components/Container');
const BulkUpdate = () => import('@/views/BulkUpdate');


Vue.use(Router)

export default new Router({
    mode: 'history',
    linkActiveClass: 'open active',
    routes: [
      {
        path: '/',
        redirect: '/',
        name: 'Home',
        component: Container,
        children: [
          {
            path: '/',
            name: 'Bulk Update',
            component: BulkUpdate
          }
        ]
      }
    ]
  });
