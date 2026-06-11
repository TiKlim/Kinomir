<template>
    <button class="app-button"
    @click="$emit('click')">
<slot></slot>
</button>
</template>

<script setup>
defineEmits(['click'])
</script>

<style scoped>

/* Стиль кнопки */
.app-button {
    background: #54535380;
    backdrop-filter: blur(4px);
    color: white;
    font-size: 16px;
    border: none;
    border-radius: 12px;
    cursor: pointer;
    transition: all 0.3s ease;
    width: 186px;
    height: 40px;
}
/* При наведении на кнопку */
.app-button:hover {
    background: #6BD2FF80;
}
/* При нажатии на кнопку */
.app-button:active {
    transform: scale(1.05);
    background: #00B2FF80;
    backdrop-filter: blur(4px);
    color: white;
}
</style>