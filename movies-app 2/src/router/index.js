import { createRouter, createWebHistory } from "vue-router"

import Home from "../views/Home.vue"
import Movies from "../views/Movies.vue"
import DetailsView from "../views/Reszletek.vue"

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
    },
    {
      path: "/details/:id",
      component: DetailsView
    }
  ]
})

export default router