<template>
    <h1 class="app-title" :class="size">
        <slot></slot>
    </h1>
</template>

<script setup>
defineProps({
    size: {
        type: String,
        default: 'medium',
        validator: (value) => ['small', 'medium', 'large'].includes(value)
    }
})
</script>

<style scoped>
.app-title {
    color: white;
    margin: 0;
    font-weight: 500;
    text-align: center;
    margin-top: 35px;
}

.app-title.small {
    font-size: 24px;
    line-height: 28px;
}

.app-title.medium {
    font-size: 28px;
    line-height: 32px;
}

.app-title.large {
    font-size: 32px;
    line-height: 36px;
}
</style>