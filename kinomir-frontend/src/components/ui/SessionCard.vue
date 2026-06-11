<template>
    <div class="schedule-card">
        <!-- Постер -->
        <div class="card-poster">
            <img :src="posterUrl" :alt="title">
        </div>
        <!-- Контент карточки -->
        <div class="card-info">
            <!-- Информация о фильме -->
            <div class="card-header">
                <h3 class="movie-title">{{ title }} | {{ ageRating }}</h3>
            </div>
            <!-- Сессии -->
            <div v-for="(sessions, date) in sessionsByDay" 
                :key="date" 
                class="day-session">
                <div class="day-label">{{ formatDate(date) }}</div>
                <div class="time-buttons">
                    <button 
                        v-for="session in sessions" 
                        :key="session.sessionId"
                        class="time-btn"
                        @click="handleSelectTime(session.sessionId, date, session.time)">
                        {{ session.time }}
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { computed } from 'vue'
// Пропсы
const props = defineProps({
    movieId: { type: Number, required: true },
    title: { type: String, required: true },
    posterUrl: { type: String, required: true },
    ageRating: { type: String, default: '' },
    sessionsByDay: { type: Object, required: true }
})

const emit = defineEmits(['select-time'])
// Сортировка
const sortedSessionsByDay = computed(() => {
    const sortedKeys = Object.keys(props.sessionsByDay).sort()
    const sorted = {}
    sortedKeys.forEach(key => {
        sorted[key] = props.sessionsByDay[key]
    })
    return sorted
})

// Форматирование даты
const formatDate = (dateString) => {
    const date = new Date(dateString)
    return date.toLocaleDateString('ru-RU', { day: 'numeric', month: 'long' })
}

const handleSelectTime = (sessionId, date, time) => {
    console.log('Передаю в SessionsView:', { 
        sessionId: Number(sessionId), 
        date, 
        time 
    })
    emit('select-time', { 
        sessionId: Number(sessionId), 
        date: date, 
        time: time 
    })
}
</script>

<style scoped>
/* Стиль для карточки */
.schedule-card {
    display: flex;
    gap: 25px;
    background: #111111;
    backdrop-filter: blur(4px);
    border-radius: 12px;
    padding: 20px;
    transition: transform 0.2s;
}
/* Стиль карточки при наведении */
.schedule-card:hover {
    transform: translateX(5px);
}
/* Стиль постера */
.card-poster {
    flex-shrink: 0;
    width: 120px;
}

.card-poster img {
    width: 100%;
    border-radius: 8px;
    object-fit: cover;
}
/* Стиль информации о фильме */
.card-info {
    flex: 1;
}

.card-header {
    display: flex;
    align-items: center;
    gap: 15px;
    flex-wrap: wrap;
    margin-bottom: 20px;
}

.movie-title {
    font-size: 1.3rem;
    color: white;
    margin: 0;
}

.day-session {
    margin-bottom: 15px;
}

.day-label {
    font-size: 0.9rem;
    color: lightgray;
    margin-bottom: 8px;
}
/* Стиль кнопки с сессией */
.time-buttons {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
}

.time-btn {
    background: #54535380;
    border: none;
    color: white;
    padding: 6px 16px;
    border-radius: 12px;
    font-size: 0.85rem;
    cursor: pointer;
    transition: all 0.2s;
}

.time-btn:hover {
    background: #6BD2FF80;
}

.time-btn:active {
    transform: scale(1.05);
    background: #00B2FF80;
    backdrop-filter: blur(4px);
    color: white;
}
</style>