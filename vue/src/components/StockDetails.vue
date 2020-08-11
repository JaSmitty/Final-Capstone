<template>
<div class="detail-card">
  <div id="stock-details">
    <h1>{{stock.companyName}} ({{stock.ticker}})</h1>
    <h2>
      {{stock.c}}
      <span :class="{'green': stock.c >= stock.o, 'red': stock.c < stock.o}">
        {{stock.c > stock.o ? "+" : ""}}
        {{(stock.c - stock.o).toFixed(3)}}
        ({{stock.c > stock.o ? "+" : ""}}
        {{((stock.c / stock.o) - 1).toFixed(3)}}%)
      </span>
    </h2>
    <h3>Open: {{stock.o}}</h3>
    <h3>High: {{stock.h}}</h3>
    <h3>Low: {{stock.l}}</h3>
    <h3>Previous Close: {{stock.pc}}</h3>
  </div>
</div>
</template>

<script>
export default {
  props: {
    stock: Object,
  },
};
</script>

<style>

.detail-card{
  display: flex;
  justify-content: center;
}
#stock-details{
  background: radial-gradient(#fcd5b6, 	#f06e04);
  border-radius: 7px;
  width: 750px;
  margin-top: 70px;
  
}
</style>