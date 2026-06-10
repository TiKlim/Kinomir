<template>
    <MainLayout>
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

        <template #center>
            <AppTitle size="large" class="app-title">Расписание сеансов</AppTitle>
            <div class="schedule-header">
                <!-- Фильтр по кинотеатрам -->
                <div class="theater-filter">
                    <label for="theater-select">Кинотеатр:</label>
                    <select id="theater-select" v-model="selectedTheater" @change="loadSchedule">
                        <option :value="null">Все кинотеатры</option>
                        <option v-for="theater in theaters" :key="theater.theaterId" :value="theater.theaterId">
                            {{ theater.theaterAddress }}
                        </option>
                    </select>
                </div>
            </div>

            <!-- Состояние загрузки -->
            <div v-if="loading" class="loading-state">
                Загрузка расписания...
            </div>

            <!-- Если нет сеансов -->
            <div v-else-if="schedule.length === 0" class="empty-state">
                На ближайшие дни сеансов нет
            </div>

            <!-- Список карточек расписания -->
            <div v-else class="schedule-list">
                <SessionCard
                    v-for="movie in schedule"
                    :key="movie.movieId"
                    :movie-id="movie.movieId"
                    :title="movie.movieTitle"
                    :poster-url="movie.moviePosterVertical"
                    :age-rating="movie.movieAgeRaiting"
                    :sessions-by-day="movie.sessionsByDay"
                    @select-time="onSelectTime"/>
            </div>
        </template>

        <template #right>
            <div class="right-panel">
                <AppButton class="app-button" @click="goToMovies">Фильмы</AppButton>
                <AppButton disabled class="used-button">Расписание</AppButton>
                <AppButton class="app-button">Акции</AppButton>
                <AppButton class="app-button">Новости</AppButton>
            </div>
        </template>
    </MainLayout>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import MainLayout from '@/components/layouts/MainLayout.vue'
import AppButton from '@/components/ui/AppButton.vue'
import AppLogo from '@/components/ui/AppLogo.vue'
import SessionCard from '@/components/ui/SessionCard.vue'
import AppTitle from '@/components/ui/AppTitle.vue';

const router = useRouter()

const schedule = ref([])
const theaters = ref([])
const selectedTheater = ref(null)
const loading = ref(true)

const loadSchedule = async () => {
    loading.value = true
    try {
        let url = 'http://localhost:5057/api/sessions/schedule'
        if (selectedTheater.value) {
            url += `?theaterId=${selectedTheater.value}`
        }
        const response = await axios.get(url)
        schedule.value = response.data
        console.log('Расписание загружено:', schedule.value)
    } catch (error) {
        console.error('Ошибка загрузки расписания:', error)
        schedule.value = []
    } finally {
        loading.value = false
    }
}

const loadTheaters = async () => {
    try {
        const response = await axios.get('http://localhost:5057/api/theaters')
        theaters.value = response.data
        console.log('Кинотеатры:', theaters.value)
    } catch (error) {
        console.error('Ошибка загрузки кинотеатров:', error)
    }
}

const onSelectTime = (sessionInfo) => {
    router.push(`/booking?movieId=${sessionInfo.movieId}&date=${sessionInfo.date}&time=${sessionInfo.time}`)
}

const goToMain = () => router.push('/')
const goToMovies = () => router.push('/movies')

onMounted(() => {
    loadTheaters()
    loadSchedule()
})
</script>

<style scoped>
.schedule-header {
    padding: 20px;
    color: white;
}

.app-title {
    text-align: left;
    padding: 16px;
    margin-top: 0px;
}

.theater-filter {
    display: flex;
    align-items: center;
    gap: 15px;
    margin-bottom: 20px;
}

.theater-filter label {
    font-size: 1rem;
    color: #ccc;
}

.theater-filter select {
    padding: 8px 16px;
    background: rgba(0, 0, 0, 0.5);
    color: white;
    border: 1px solid #00B2FF;
    border-radius: 8px;
    cursor: pointer;
}

.schedule-list {
    display: flex;
    flex-direction: column;
    gap: 20px;
    padding: 20px;
}

.loading-state,
.empty-state {
    color: white;
    text-align: center;
    padding: 60px;
    font-size: 1.2rem;
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

.used-button {
    background: #00B2FF80;
}
</style>