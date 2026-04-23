import { createRouter, createWebHistory } from "vue-router"

import Home from "../views/Home.vue"
import Movies from "../views/Movies.vue"

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      component: Home
    },
    {
      path: "/movies",
      component: Movies
    }
  ]
})

export default router