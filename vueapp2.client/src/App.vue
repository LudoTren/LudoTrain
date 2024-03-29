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
    <header>
        <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

        <div class="wrapper">

            <input v-model="message" v-on:keyup.enter="updateHelloWorld" placeholder="edit me" />
            <button @click="updateHelloWorld">Submit</button>
            <br />  
            
        </div>
    </header>

    <main>
        <HelloWorld :msg="message" :key="helloWorldKey" />
        <!--<TheWelcome />-->
    </main>
</template>

<style scoped>
    header {
        line-height: 1.5;
    }

    .logo {
        display: block;
        margin: 0 auto 2rem;
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
</style>
