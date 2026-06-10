<template>
    <MainLayout>
        <!-- Левая колонка -->
        <template #left>
            <div class="left-panel">
                <AppLogo class="special-button" text-color="#00B2FF" font-size="46px">
                    <span class="logo-text">
                        КИН<img src="@/assets/theaters.svg" class="inline-icon">МИР
                    </span>
                </AppLogo>
                <AppButton class="app-button" @click="goToMain">Главная</AppButton>
            </div>
        </template>

        <!-- Центральная колонка -->
        <template #center>
            <!-- Состояние загрузки -->
            <div v-if="loading" class="loading-state">
                Загрузка фильмов...
            </div>
            
            <!-- Если фильмов нет -->
            <div v-else-if="movies.length === 0" class="empty-state">
                Скоро здесь появятся новые фильмы
            </div>
            
            <!-- Сетка с фильмами -->
            <div v-else class="movies-grid">
                <MovieCard 
                    v-for="movie in movies" 
                    :key="movie.movieId"
                    :id="movie.movieId"
                    :title="movie.movieTitle"
                    :posterUrl="movie.moviePosterVertical"
                    :year="movie.movieReleaseYear"
                    :duration="movie.movieDuration"
                    :ageRating="movie.movieAgeRaiting"
                    @click="goToMovie"
                />
            </div>
        </template>

        <!-- Правая колонка -->
        <template #right>
            <div class="right-panel">
                <AppButton disabled class="used-button">Фильмы</AppButton>
                <AppButton class="app-button">Расписание</AppButton>
                <AppButton class="app-button">Акции</AppButton>
                <AppButton class="app-button">Новости</AppButton>
            </div>
        </template>
    </MainLayout>
</template>

<script setup>
import MainLayout from '@/components/layouts/MainLayout.vue';
import AppButton from '@/components/ui/AppButton.vue';
import AppLogo from '@/components/ui/AppLogo.vue';
import MovieCard from '@/components/ui/MovieCard.vue';
import axios from 'axios'
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

// Хранение фильмов
const movies = ref([])
const loading = ref(true)

// Загрузка фильмов
const loadMovies = async () => {
    loading.value = true
    try {
        const response = await axios.get('http://localhost:5057/api/movies/now')
        movies.value = response.data
        console.log('Загружено фильмов:', movies.value.length)
        console.log('Raw API response:', response.data);
        console.log('Array length:', response.data.length);
        console.log('Первый элемент массива movies:', movies.value[0])
    } catch (error) {
        console.error('Ошибка загрузки фильмов:', error)
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

onMounted(() => {
    loadMovies()
})
</script>

<style scoped>
/* Глобальные стили */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

.used-button {
    background: #00B2FF80;
}

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