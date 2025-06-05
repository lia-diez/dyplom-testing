<script setup lang="ts">

const examples = [
  {
    image: 'dnd.png',
    label: 'Згенеруй мапу для D&D гри про піратів',
    position: 'right'
  },
  {
    image: 'island.png',
    label: '5 скелястих островів',
    position: 'left'
  },
  {
    image: 'mount.png',
    label: 'Епічний гірний ландшафт',
    position: 'right'
  }

]

</script>

<template>
  <div class="container">

    <div class="content-list">
      <!--      <Menu :model="items" style="border-radius: 2rem; padding: 1rem;"/>-->
    </div>
    <div class="main">
      <panel style="border-radius: 2rem; padding: 1rem;">
        <Carousel :value="examples"  circular :autoplayInterval="3000">
          <template #item="slotProps">
            <div class="image-comment">
              <div v-if="slotProps.data.position === 'left'" class="qoute-container">
                <div class="qoutemark">"<br/></div>
                <div class="qoute">{{ slotProps.data.label }}</div>
                <div class="qoutemark" style="text-align: end">"</div>
              </div>
              <img :src="'/' + slotProps.data.image" :alt='slotProps.data.image'/>
              <div v-if="slotProps.data.position === 'right'" class="qoute-container">
                <div class="qoutemark">"<br/></div>
                <div class="qoute">{{ slotProps.data.label }}</div>
                <div class="qoutemark" style="text-align: end">"</div>
              </div>
            </div>
          </template>
        </Carousel>
        <p>
          TerraLingua — це програмний комплекс для створення 3D-мап на основі текстового опису. Поєднання LLM та точного
          процедурного генератора дозволяє будь-кому сформувати унікальний ландшафт, готовий до використання в іграх.
        </p>
        <p>Наразі генератор можна використати для генерації об'єктів Terrain в Unity. Для цього потрібно:</p>
        <ul>
          <li>Зареєструватися на сайті</li>
          <li>Отримати API-ключ в профілі користувача</li>
          <li><a href="https://docs.unity3d.com/6000.1/Documentation/Manual/upm-ui-actions.html" target="_blank"
                         rel="noopener noreferrer">Додати </a>
            до власного проєкту пакет з GitHub-репозиторію;
          </li>
          <li>У меню "Tools" Unity обрати "Terrain Generator"</li>
          <li>Ввести запит та API-ключ</li>
        </ul>


      </panel>
    </div>
  </div>

</template>

<style scoped>
.container {
  display: grid;
  grid-template-columns: 1fr 3fr 1fr;
}

.main {
  grid-column: 2;
  margin-top: 1rem;
  width: 60vw;
}

.content-list {
  grid-column: 1;
  margin-top: 3rem;
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.image-comment {
  display: grid;
  grid-template-columns: 1fr 1fr;
  padding: 1rem;
}

img {
  border-radius: 2rem;
  width: 100%;
}

.qoute-container {
  padding: 2rem;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  font-size: 2rem;
}

.qoute {
  padding-left: 2rem;
  padding-right: 2rem;
  text-align: center;
}

.qoutemark {
  font-size: 5rem;
  width: 100%;
  margin-bottom: -3rem;
  margin-top: -2rem;
  font-style: italic;
  color: var(--p-badge-primary-background);
}

a {
  color: var(--p-badge-primary-background);
  text-decoration: underline;
}
</style>