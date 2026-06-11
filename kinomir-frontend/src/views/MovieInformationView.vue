<template>
    <MainLayout>
        <!-- Левая колонка -->
        <template #left>
            <div class="left-panel">
                <!-- Лого -->
                <AppLogo class="special-button" text-color="#00B2FF" font-size="46px">
                    <span class="logo-text">
                        КИН<img src="@/assets/theaters.svg" class="inline-icon">МИР
                    </span>
                </AppLogo>
                <!-- Кнопки -->
                <AppButton class="app-button" @click="goToMain">Главная</AppButton>
                <AppButton class="app-button" @click="goToMovies">Назад</AppButton>
            </div>
        </template>
        <!-- Центральная колонка -->
        <template #center>
            <!-- Состояние загрузки -->
            <div v-if="loading" class="loading-state">
                Загрузка...
            </div>
            <!-- Информация о фильме -->
            <div v-else-if="movie" class="movie-detail">
                <div class="movie-poster-horizontal">
                    <img :src="movie.moviePosterHorizontal" :alt="movie.movieTitle">
                </div>
                <!-- Название фильма -->
                <div class="movie-info">
                    <h1 class="movie-title">{{ movie.movieTitle }}</h1>
                    <!-- Возрастной рейтинг, год релиза, длительность в минутах -->
                    <div class="movie-meta">
                        <span class="movie-age">{{ movie.movieAgeRaiting }}</span>
                        <span class="movie-year">{{ movie.movieReleaseYear }} г.</span>
                        <span class="movie-duration">{{ movie.movieDuration }} мин</span>
                    </div>
                    <div class="movie-tags" v-if="movie.tags && movie.tags.length">
                        <span class="tags-list">{{ movie.tags.join(', ') }}</span>
                    </div>
                    <!-- Описание фильма -->
                    <p class="movie-description">{{ movie.movieDescription }}</p>
                </div>
            </div>
        </template>
        <!-- Правая колонка -->
        <template #right>
            <div class="right-panel">
                <AppButton disabled class="used-button">Фильмы</AppButton>
                <AppButton class="app-button" @click="goToSessions">Расписание</AppButton>
                <AppButton class="app-button" @click="goToPromotions">Акции</AppButton>
                <AppButton class="app-button" @click="goToNews">Новости</AppButton>
            </div>
        </template>
    </MainLayout>
</template>

<script setup>
// Импорты
import MainLayout from '@/components/layouts/MainLayout.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppLogo from '@/components/ui/AppLogo.vue';
import axios from 'axios'
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// Router
const route = useRoute()
const router = useRouter()
// Получаем данные о фильме
const movie = ref(null)
const loading = ref(true)
const movieId = route.params.id
// Загрузка фильма
const loadMovie = async () => {
    loading.value = true
    try {
        const response = await axios.get(`http://localhost:5057/api/movies/${movieId}`)
        movie.value = response.data
        console.log('Загружен фильм:', movie.value)
    } catch (error) {
        console.error('Ошибка загрузки фильма:', error)
        movie.value = null
    } finally {
        loading.value = false
    }
}
// Переход на страницу фильма
const goToMovie = (movieId) => {
    router.push(`/movie/${movieId}`)
}
const goToMain = () => {
    router.push('/')
}
const goToMovies = () => {
    router.push('/movies')
}
const goToSessions = () => {
    router.push('/sessions')
}
const goToPromotions = () => router.push('/promotions')
const goToNews = () => router.push('/news')

onMounted(() => {
    loadMovie()
})
</script>

<style scoped>
/* Глобальные стили */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
/* Стиль для контейнера */
.movie-detail {
    display: flex;
    flex-direction: column;
    gap: 30px;
    padding: 30px;
    border-radius: 16px;
    margin: 20px;
}
/* Горизонтальный постер */
.movie-poster-horizontal {
    width: 100%;
    max-height: 400px;
    overflow: hidden;
    border-radius: 12px;
}

.movie-poster-horizontal img {
    width: 100%;
    height: auto;
    object-fit: cover;
}
/* Стиль для информации о фильме */
.movie-info {
    color: white;
}

.movie-title {
    font-size: 2rem;
    margin-bottom: 15px;
}

.movie-meta {
    display: flex;
    gap: 20px;
    margin-bottom: 15px;
    flex-wrap: wrap;
}

.movie-year, .movie-age, .movie-duration {
    background: rgba(255, 255, 255, 0.1);
    padding: 4px 12px;
    border-radius: 12px;
}
/* Теги */
.movie-tags {
    margin-bottom: 20px;
}

.tags-label {
    color: #888;
    margin-right: 10px;
}

.tags-list {
    color: lightgrey;
}
/* Стиль для описания фильма */
.movie-description {
    line-height: 1.6;
    color: lightgrey;
    margin-bottom: 30px;
}
/* Стиль для статичной выбранной кнопки */
.used-button {
    background: #00B2FF80;
}
/* Стиль для заголовка */
.app-title {
    text-align: left;
    padding: 16px;
    margin-top: 0px;
}

.movies-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(306px, 1fr));
    gap: 24px;
    padding: 16px;
}
/* Стиль лого */
.logo-text {
    display: inline-flex;
    align-items: center;
    gap: 0;
}

.inline-icon {
    width: 16px;
    height: 16px;
    display: block;
    margin: 0 2px;
}

body {
    font-family: Arial, sans-serif;
}
/* Стили для левого меню */
.left-panel {
    display: flex;
    flex-direction: column;
    gap: 15px;
    align-items: center;
    margin-top: 15px;
}
/* Стили для правой панели */
.right-panel {
    color: white;
    display: flex;
    flex-direction: column;
    gap: 15px;
    align-items: center;
    margin-top: 15px;
}

.loading-state,
.empty-state {
    color: white;
    text-align: center;
    padding: 40px;
    font-size: 1.2rem;
}
</style>