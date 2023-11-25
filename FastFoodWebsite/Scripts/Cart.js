const price = document.querySelector(".price");
const changePrice = document.querySelector(".changePrice");

changePrice.addEventListener("change", (e) => {
    const quantity = e.target.value * 1;
    const priceTxt = price.textContent * 1;
    const result = quantity * priceTxt;

    price.textContent = result.toFixed(2);

});