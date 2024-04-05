<script setup>
    import HelloWorld from './components/HelloWorld.vue'
    import TheWelcome from './components/TheWelcome.vue'

    import { ref } from 'vue'

    const message = ref('')
    const helloWorldKey = ref(0)
    const updateHelloWorld = () => {
        helloWorldKey.value++
    }
</script>

<template>
    <div id="app">
        <div class="content">
            <input v-model="message" v-on:keyup.enter="updateHelloWorld" placeholder="edit me" />
            <button @click="updateHelloWorld">Submit</button>
            <HelloWorld :msg="message" :key="helloWorldKey" />
        </div>
    </div>
</template>

<style scoped>
    header {
        line-height: 1.5;
    }

    .logo {
        display: block;
        margin: 0 auto 2rem;
    }
    #app {
        display: flex;
        flex-direction: row;
        height: 100vh;
    }
    @media (min-width: 1024px) {
        header {
            display: flex;
            place-items: center;
            padding-right: calc(var(--section-gap) / 2);
        }

        .logo {
            margin: 0 2rem 0 0;
        }

        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }
    .sidebar {
        width: 200px;
        background-color: #333;
        color: #fff;
        margin:0 2rem 0 0;
    }

    .sidebar ul {
        list-style-type: none;
        padding: 0;
    }

    .sidebar li {
        padding: 10px;
    }

    .content {
        flex: 1;
        padding: 5px;
    }
</style>
