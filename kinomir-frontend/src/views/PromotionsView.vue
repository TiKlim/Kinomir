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
            <div v-if="loading" class="loading-state">
                Загрузка акций...
            </div>

            <div v-else-if="promotions.length === 0" class="empty-state">
                Акций пока нет
            </div>

            <div v-else class="promotions-list">
                <div v-for="promo in promotions" :key="promo.promotionId" class="promotion-card">
                    <p class="promotion-content">{{ promo.promotionTitle }}</p>
                    <p class="promotion-content">{{ promo.promotionContent }}</p>
                </div>
            </div>
        </template>

        <template #right>
            <div class="right-panel">
                <AppButton class="app-button" @click="goToMovies">Фильмы</AppButton>
                <AppButton class="app-button" @click="goToSchedule">Расписание</AppButton>
                <AppButton disabled class="used-button">Акции</AppButton>
                <AppButton class="app-button" @click="goToNews">Новости</AppButton>
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
import AppTitle from '@/components/ui/AppTitle.vue'

const router = useRouter()

const promotions = ref([])
const loading = ref(true)

const loadPromotions = async () => {
    loading.value = true
    try {
        const response = await axios.get('http://localhost:5057/api/promotions')
        promotions.value = response.data
        console.log('Акции загружены:', promotions.value)
    } catch (error) {
        console.error('Ошибка загрузки акций:', error)
        promotions.value = []
    } finally {
        loading.value = false
    }
}

const goToMain = () => router.push('/')
const goToMovies = () => router.push('/movies')
const goToSchedule = () => router.push('/sessions')
const goToNews = () => router.push('/news')

onMounted(() => {
    loadPromotions()
})
</script>

<style scoped>
.app-title {
    text-align: left;
    padding: 16px;
    margin-top: 0px;
}

.promotions-list {
    display: flex;
    flex-direction: column;
    gap: 20px;
    padding: 20px;
    background: #111111;
    backdrop-filter: blur(4px);
    border-radius: 12px;
}

.promotion-card {
    padding: 24px;
    transition: transform 0.2s;
}

.promotion-card:hover {
    transform: translateX(5px);
}

.promotion-content {
    color: white;
    text-align: center;
    font-size: 18px;
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