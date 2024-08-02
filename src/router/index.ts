import { useUserStore } from "@/store/useUserStore";
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "login",
    meta: {
      requireAuth: false,
    },
    component: () =>
      import(/*webpackChunkName: "login" */ "../views/login/LoginView.vue"),
  },
  {
    path: "/recovery",
    name: "recovery",
    meta: {
      requireAuth: false,
    },
    component: () =>
      import(
        /*webpackChunkName: "login" */ "../views/recovery/RecoveryView.vue"
      ),
  },
  {
    path: "/dashboard",
    name: "dashboard",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../views/dashboard/DashboardView.vue"
      ),
  },
  {
    path: "/groups",
    name: "groups",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "about" */ "../pages/groups/GroupsPage.vue"),
  },
  {
    path: "/groups/:id",
    name: "group",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "about" */ "../pages/groups/GroupsForm.vue"),
  },
  {
    path: "/home",
    name: "home",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "about" */ "../pages/home/HomePage.vue"),
  },
  {
    path: "/users",
    name: "users",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "about" */ "../pages/users/UsersPage.vue"),
  },
  {
    path: "/users/:id",
    name: "user",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "about" */ "../pages/users/UserPage.vue"),
  },
  {
    path: "/students",
    name: "students",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../pages/students/StudentsPage.vue"
      ),
  },
  {
    path: "/students/:id",
    name: "student",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../pages/students/StudentPage.vue"
      ),
  },
  {
    path: "/attendance",
    name: "attendance",
    meta: {
      requireAuth: true,
    },
    component: () =>
      import(
        /* webpackChunkName: "about" */ "../pages/attendance/AttendancePage.vue"
      ),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const auth = useUserStore().currentUser !== "";
  const needAuth = to.meta.requireAuth;
  if (needAuth && !auth) {
    next("/"); // se agrega el name de la ruta
  } else {
    next();
  }
});

export default router;
