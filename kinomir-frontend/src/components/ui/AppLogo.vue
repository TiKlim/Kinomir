<template>
    <button class="logo-button"
    @click="$emit('click')">
    <slot></slot>
    </button>
</template>

<script setup>
defineEmits(['click'])
</script>

<style scoped>
.logo-button {
    background: #11111180;
    backdrop-filter: blur(4px);
    font-size: 16px;
    color: #00B2FF;
    font-family: 'Unbounded', sans-serif;
    border: 2px solid #00B2FF;
    border-radius: 12px;
    cursor: pointer;
    transition: all 0.3s ease;
    width: 186px;
    height: 40px;
}
</style>