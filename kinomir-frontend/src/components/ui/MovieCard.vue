<template>
    <div class="movie-card" @click="handleClick">
        <!-- Постер -->
        <div class="poster-wrapper">
            <img 
                :src="posterUrl" 
                :alt="title"
                class="movie-poster"
                @error="handleImageError">
        </div>
        <!-- Информация о фильме -->
        <div class="movie-info">
            <h3 class="movie-title">{{ title }}</h3>
            <p v-if="ageRating" class="movie-age">{{ ageRating }}</p>
        </div>
    </div>
</template>

<script setup>
// Пропсы
const props = defineProps({
    id: {
        type: Number,
        required: true
    },
    title: {
        type: String,
        required: true
    },
    posterUrl: {
        type: String,
        default: ''
    },
    year: {
        type: Number,
        default: null
    },
    duration: {
        type: Number,
        default: null
    },
    ageRating: {
        type: String,
        default: ''
    }
})

const emit = defineEmits(['click'])

const handleClick = () => {
    emit('click', props.id)
}

const handleImageError = (event) => {
    event.target.src = '/placeholder-poster.jpg'
}
</script>

<style scoped>
/* Стиль карточки */
.movie-card {
    cursor: pointer;
    transition: transform 0.2s, box-shadow 0.2s;
    background: #54535380;
    backdrop-filter: blur(4px);
    border-radius: 12px;
    overflow: hidden;
    width: 100%;
}
/* Стиль при наведении на кнопку */
.movie-card:hover {
    transform: scale(1.02);
    background: #6BD2FF80;
}
/* Стиль при нажатии на кнопку */
.movie-card:active {
    transform: scale(1.05);
    background: #00B2FF80;
}
/* Стиль постера */
.poster-wrapper {
    position: relative;
    width: 100%;
    padding-top: 150%;
    overflow: hidden;
}

.movie-poster {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
}
/* Стиль для информации о фильме */
.movie-info {
    padding: 12px;
    text-align: center;
}

.movie-title {
    color: white;
    font-size: 1rem;
    margin: 0 0 8px 0;
    font-weight: 600;
    text-align: left;
}

.movie-year,
.movie-duration,
.movie-age {
    color: lightgray;
    font-size: 0.8rem;
    margin: 4px 0;
    text-align: left;
}
</style>