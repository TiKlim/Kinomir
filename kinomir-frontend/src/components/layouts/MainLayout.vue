<template>
    <div class="three-column-layout">
        <!-- Левая колонка -->
        <div class="column left-column">
            <div class="column-content">
                <slot name="left"></slot>
            </div>
        </div>

        <!-- Разделитель 1 -->
        <div class="divider"></div>

        <!-- Центральная колонка -->
        <div class="column center-column">
            <div class="column-content">
                <slot name="center"></slot>
            </div>
        </div>

        <!-- Разделитель 2 -->
        <div class="divider"></div>

        <!-- Правая колонка -->
        <div class="column right-column">
            <div class="column-content">
                <slot name="right"></slot>
            </div>
        </div>
    </div>
</template>

<script setup>
</script>

<style scoped>
.three-column-layout {
    display: flex;
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background: black;
}

/* Общие стили для всех колонок */
.column {
    flex-shrink: 0; 
    overflow-y: auto;
    position: relative;
}

/* Левая колонка */
.left-column {
    flex-basis: 20%;
}

/* Центральная колонка */
.center-column {
    flex-basis: 60%;
}

/* Правая колонка */
.right-column {
    flex-basis: 20%;
}

/* Контент внутри колонки */
.column-content {
    padding: 20px;
    height: 100%;
    box-sizing: border-box;
}

/* Вертикальные разделители */
.divider {
    width: 1px;
    background: white;
    opacity: 0.3;
    flex-shrink: 0;
}
</style>