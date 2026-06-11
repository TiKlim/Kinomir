<template>
    <MainLayout>
        <template #left>
            <div class="left-panel">
                <AppLogo class="special-button" text-color="#00B2FF" font-size="46px">
                    <span class="logo-text">
                        КИН<img src="@/assets/theaters.svg" class="inline-icon">МИР
                    </span>
                </AppLogo>
                <AppButton class="app-button" @click="goBack">Назад</AppButton>
            </div>
        </template>

        <template #center>
            <div v-if="loading" class="loading-state">Загрузка схемы зала...</div>
            
            <div v-else-if="sessionInfo" class="hall-container">
                <!-- Информация о сеансе -->
                <div class="session-info">
                    <h2>{{ sessionInfo.hallName }}</h2>
                    <p>{{ formatDate(sessionInfo.sessionDate) }} | {{ sessionInfo.sessionTime }}</p>
                </div>

                <!-- Экран -->
                <div class="screen-wrapper">
                    <div class="screen"></div>
                    <div class="screen-label">ЭКРАН</div>
                </div>

                <!-- Схема зала -->
                <div class="seats-container">
                    <div 
                        v-for="row in rows" 
                        :key="row.number" 
                        class="seat-row">
                        <!-- Левая колонка с номером ряда -->
                        <div class="row-label left">Ряд {{ row.number }}</div>
                        
                        <!-- Места в ряду -->
                        <div class="seats">
                            <div 
                                v-for="seat in row.seats" 
                                :key="seat.number"
                                class="seat"
                                :class="{
                                    'available': seat.isAvailable,
                                    'booked': !seat.isAvailable,
                                    'selected': selectedSeat?.row === row.number && selectedSeat?.number === seat.number}"
                                @click="selectSeat(row.number, seat.number, seat.isAvailable)">
                                {{ seat.number }}
                            </div>
                        </div>
                        
                        <!-- Правая колонка с номером ряда -->
                        <div class="row-label right">Ряд {{ row.number }}</div>
                    </div>
                </div>

                <!-- Форма бронирования -->
                <div v-if="selectedSeat" class="booking-form">
                    <h3>Бронирование места</h3>
                    <p>Ряд {{ selectedSeat.row }}, место {{ selectedSeat.number }}</p>
                    <div class="form-group">
                        <label>Телефон:</label>
                        <input v-model="userPhone" type="tel" placeholder="+7 (XXX) XXX-XX-XX" required>
                    </div>
                    <div class="form-group">
                        <label>Email:</label>
                        <input v-model="userEmail" type="email" placeholder="example@mail.ru" required>
                    </div>
                    <AppButton class="book-btn" @click="createBooking" :disabled="!canBook">
                        Забронировать
                    </AppButton>
                </div>
            </div>
        </template>

        <template #right>
            <div class="right-panel">
                <AppButton class="app-button" @click="goToMovies">Фильмы</AppButton>
                <AppButton disabled class="used-button">Расписание</AppButton>
                <AppButton class="app-button" @click="goToPromotions">Акции</AppButton>
                <AppButton class="app-button" @click="goToNews">Новости</AppButton>
            </div>
        </template>
    </MainLayout>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'
import MainLayout from '@/components/layouts/MainLayout.vue'
import AppButton from '@/components/ui/AppButton.vue'
import AppLogo from '@/components/ui/AppLogo.vue'

const route = useRoute()
const router = useRouter()

const sessionId = route.query.sessionId
console.log('sessionId из URL:', sessionId, 'тип:', typeof sessionId)
const loading = ref(true)
const sessionInfo = ref(null)
const bookedSeats = ref([])  // Занятые места
const selectedSeat = ref(null)
const userPhone = ref('')
const userEmail = ref('')
const bookingSuccess = ref(false)

// Конфигурация зала (ряды и количество мест)
const hallConfig = [
    { row: 1, seats: 8 },
    { row: 2, seats: 8 },
    { row: 3, seats: 8 },
    { row: 4, seats: 8 },
    { row: 5, seats: 12 },
    { row: 6, seats: 12 },
    { row: 7, seats: 12 }
]

// Генерация рядов с учётом занятых мест
const rows = computed(() => {
    return hallConfig.map(rowConfig => {
        const seats = []
        for (let i = 1; i <= rowConfig.seats; i++) {
            // Проверяем, занято ли место
            const isBooked = bookedSeats.value.some(booking => {
                // Пробую разные варианты названий полей
                const bookingRow = booking.rowNumber ?? booking.row
                const bookingSeat = booking.seatNumber ?? booking.seat
                return Number(bookingRow) === rowConfig.row && Number(bookingSeat) === i
            })
            seats.push({
                number: i,
                isAvailable: !isBooked
            })
        }
        return {
            number: rowConfig.row,
            seats: seats
        }
    })
})

// Проверка, можно ли бронировать
const canBook = computed(() => {
    return selectedSeat.value && userPhone.value && userEmail.value
})

// Загрузка информации о сеансе и занятых местах
const loadSessionData = async () => {
    loading.value = true
    try {
        // Загружаем информацию о сеансе (зал, дата, время)
        const sessionResponse = await axios.get(`http://localhost:5057/api/sessions/${sessionId}`)
        sessionInfo.value = sessionResponse.data
        
        // Загружаем занятые места для этого сеанса
        const seatsResponse = await axios.get(`http://localhost:5057/api/bookings/session/${sessionId}`)
        bookedSeats.value = seatsResponse.data
        console.log('Занятые места:', bookedSeats.value)
    } catch (error) {
        console.error('Ошибка загрузки данных:', error)
    } finally {
        loading.value = false
    }
}

// Выбор места
const selectSeat = (row, seat, isAvailable) => {
    if (!isAvailable) return  // Нельзя выбрать занятое место
    if (selectedSeat.value?.row === row && selectedSeat.value?.number === seat) {
        selectedSeat.value = null  // Снять выбор
    } else {
        selectedSeat.value = { row, number: seat }
    }
}

// Создание бронирования
const createBooking = async () => {
    if (!canBook.value) return
    
    try {
        await axios.post('http://localhost:5057/api/bookings', {
            sessionId: parseInt(sessionId),
            rowNumber: selectedSeat.value.row,
            seatNumber: selectedSeat.value.number,
            userPhone: userPhone.value,
            userEmail: userEmail.value
        })
        
        bookingSuccess.value = true
        alert('Билет успешно забронирован!')
        
        // Перезагружаем занятые места
        await loadSessionData()
        selectedSeat.value = null
        userPhone.value = ''
        userEmail.value = ''
    } catch (error) {
        console.error('Ошибка бронирования:', error)
        alert('Не удалось забронировать место. Попробуйте ещё раз.')
    }
}

// Форматирование даты
const formatDate = (dateString) => {
    const date = new Date(dateString)
    return date.toLocaleDateString('ru-RU', { day: 'numeric', month: 'long', year: 'numeric' })
}

// Навигация
const goBack = () => router.back()
const goToMovies = () => router.push('/movies')
const goToPromotions = () => router.push('/promotions')
const goToNews = () => router.push('/news')

onMounted(() => {
    loadSessionData()
})
</script>

<style scoped>
.hall-container {
    padding: 20px;
    color: white;
}

.session-info {
    text-align: center;
    margin-bottom: 30px;
}

.session-info h2 {
    font-size: 1.5rem;
    margin-bottom: 8px;
}

.session-info p {
    color: white;
    font-size: 1rem;
}

/* Экран */
.screen-wrapper {
    text-align: center;
    margin-bottom: 40px;
}

.screen {
    width: 80%;
    height: 8px;
    background: linear-gradient(90deg, gray, lightgray, gray);
    margin: 0 auto;
    border-radius: 4px;
}

.screen-label {
    margin-top: 8px;
    color: lightgray;
    font-size: 0.8rem;
    letter-spacing: 4px;
}

/* Схема зала */
.seats-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 12px;
    margin-bottom: 40px;
    overflow-x: auto;
}

.seat-row {
    display: flex;
    align-items: center;
    gap: 20px;
}

.row-label {
    width: 60px;
    font-size: 0.8rem;
    color: lightgray;
}

.row-label.left {
    text-align: right;
}

.row-label.right {
    text-align: left;
}

.seats {
    display: flex;
    gap: 8px;
    flex-wrap: wrap;
    justify-content: center;
}

.seat {
    width: 40px;
    height: 40px;
    display: flex;
    align-items: center;
    justify-content: center;
    background: #54535380;
    border-radius: 8px;
    cursor: pointer;
    font-size: 0.9rem;
    transition: all 0.2s;
}

.seat.available:hover {
    background: #6BD2FF80;
    transform: scale(1.05);
}

.seat.booked {
    background: #00B2FF80;
    cursor: not-allowed;
    opacity: 0.6;
}

.seat.selected {
    background: #00B2FF80;
    transform: scale(1.05);
}

/* Форма бронирования */
.booking-form {
    max-width: 400px;
    margin: 0 auto;
    padding: 8px;
    border-radius: 12px;
    text-align: center;
}

.booking-form h3 {
    margin-bottom: 15px;
}

.booking-form p {
    margin-bottom: 20px;
    color: #00B2FF;
}

.form-group {
    margin-bottom: 15px;
    text-align: left;
}

.form-group label {
    display: block;
    margin-bottom: 5px;
    font-size: 0.9rem;
    color: white;
}

.form-group input {
    width: 100%;
    padding: 10px;
    background: #111111;
    border: 1px solid #333;
    border-radius: 6px;
    color: white;
    font-size: 1rem;
}

.form-group input:focus {
    outline: none;
    border-color: #00B2FF;
}

.book-btn {
    width: 100%;
    margin-top: 10px;
}

.loading-state {
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