import { createRouter, createWebHistory } from 'vue-router'
import MainView from '../views/MainView.vue'
import MoviesView from '@/views/MoviesView.vue'
import MovieInformationView from '@/views/MovieInformationView.vue'
import SessionsView from '@/views/SessionsView.vue'
import HallView from '@/views/HallView.vue'
import PromotionsView from '@/views/PromotionsView.vue'
import NewsView from '@/views/NewsView.vue'

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
    {
      path: '/sessions',
      name: 'sessions',
      component: SessionsView,
    },
    {
      path: '/hall',
      name: 'hall',
      component: HallView,
    },
    {
        path: '/promotions',
        name: 'promotions',
        component: PromotionsView
    },
    {
        path: '/news',
        name: 'news',
        component: NewsView
    }
  ],
})

export default router
