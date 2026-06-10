import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import MoviesView from '@/views/MoviesView.vue'
import MovieInformationView from '@/views/MovieInformationView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: MainView,
    },
    {
      path: '/movies',
      name: 'movies',
      component: MoviesView,
    },
    {
      path: '/movie/:id',
      name: 'info',
      component: MovieInformationView,
    },
  ],
})

export default router
