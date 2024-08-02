import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'login',
    component: () => import(/*webpackChunkName: "login" */ '../views/login/LoginView.vue')
  },
  {
    path: '/recovery',
    name: 'recovery',
    component: () => import(/*webpackChunkName: "login" */ '../views/recovery/RecoveryView.vue')
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: () => import(/* webpackChunkName: "about" */ '../views/dashboard/DashboardView.vue')
  },
  {
    path: '/groups',
    name: 'groups',
    component: () => import(/* webpackChunkName: "about" */ '../pages/groups/GroupsPage.vue')
  },
  {
    path: '/groups/:id',
    name: 'group',
    component: () => import(/* webpackChunkName: "about" */ '../pages/groups/GroupsForm.vue')
  },
  {
    path: '/home',
    name: 'home',
    component: () => import(/* webpackChunkName: "about" */ '../pages/home/HomePage.vue')
  },
  {
    path: '/users',
    name: 'users',
    component: () => import(/* webpackChunkName: "about" */ '../pages/users/UsersPage.vue')
  },
  {
    path: '/users/:id',
    name: 'user',
    component: () => import(/* webpackChunkName: "about" */ '../pages/users/UserPage.vue')
  },
  {
    path: '/students',
    name: 'students',
    component: () => import(/* webpackChunkName: "about" */ '../pages/students/StudentsPage.vue')
  },
  {
    path: '/students/:id',
    name: 'student',
    component: () => import(/* webpackChunkName: "about" */ '../pages/students/StudentPage.vue')
  },
  {
    path: '/attendance',
    name: 'attendance',
    component: () => import(/* webpackChunkName: "about" */ '../pages/attendance/AttendancePage.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
