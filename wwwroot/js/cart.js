if (document.readyState == "loading") {
  document.addEventListener("DOMContentLoaded", ready);
} else {
  ready();
}
var removeCartButton = document.getElementsByClassName("btn-delete");
var minusButton = document.getElementsByClassName("btn-minus");
var plusButton = document.getElementsByClassName("btn-plus");
var quantityInput = document.getElementsByClassName("quantity-input");
var costColumn = document.getElementsByClassName("cost");
var cashTotalOfItem = document.getElementsByClassName("cartItemCashTotal");
var subTotalTag = document.getElementById("TotalCart");
var shippingTag = document.getElementById("shipping");
var totalCartTag = document.getElementById("totalCart");
// Define ready as a function declaration
function ready() {
  var subtotal = 0;
  for (var i = 0; i < removeCartButton.length; i++) {
    (function (i) {
      // IIFE to create a new scope
      var button = removeCartButton[i];
      var minusBtn = minusButton[i];
      var plusBtn = plusButton[i];
      var costCol = parseFloat(
        costColumn[i].innerHTML.replace("$", "").replace(",", ".")
      );
      var quantityInp = quantityInput[i];
      var quantityInputValue = quantityInput[i].value;
      var cashTotalOfIte = cashTotalOfItem[i];
      quantityInp.addEventListener("change", (event) => {
        if (validateNoAlphabetic(event.target.value) == true) {
          if (Number(quantityInp.value) > 99) {
            quantityInputValue = 99;
            quantityInp.value = quantityInputValue;
          }
          updateAllTotal(event, costCol, cashTotalOfIte);
          quantityInputValue = event.target.value;
          subTotal(cashTotalOfIte);
        } else {
          alert("just a number");
          quantityInputValue = 1; // Set the variable to 1
          quantityInp.value = quantityInputValue; // Update the input field value
          subTotal(cashTotalOfIte);
        }
      });
      minusBtn.addEventListener("click", (event) => {
        console.log(quantityInputValue);
        if (quantityInputValue < 0) {
          event.target.dataset.value = 0;
        } else {
          event.target.dataset.value = quantityInputValue - 1;
        }
        result = Number(event.target.dataset.value);
        if (result >= 0) {
          quantityInputValue = result;
        } else {
          quantityInputValue = 0;
        }

        updateTotalOfItem(quantityInputValue, costCol, cashTotalOfIte);
        subTotal(cashTotalOfIte);
      });
      plusBtn.addEventListener("click", (event) => {
        if (quantityInputValue >= 99) {
          alert("You want to buy item go out range!");
          quantityInp.value = 99;
          updateTotalOfItem(Number(quantityInp.value), costCol, cashTotalOfIte);
          subTotal(subtotal, cashTotalOfIte);
        } else {
          event.target.dataset.value = Number(quantityInputValue) + 1;
          quantityInputValue = Number(event.target.dataset.value);
          updateTotalOfItem(quantityInputValue, costCol, cashTotalOfIte);
          subTotal(subtotal, cashTotalOfIte);
        }
      });
      button.addEventListener("click", (event) => removeItem(event));
      subtotal =
        subtotal +
        Number(cashTotalOfIte.innerHTML.replace("$", "").replace(",", "."));
      var total =
        subtotal +
        Number(shippingTag.innerHTML.replace(",", ".").replace("$", ""));
      totalCartTag.innerHTML = "$" + total;
    })(i); // Pass the current index to the IIFE
  }
  subTotalTag.innerHTML = "$" + subtotal;
}
// Move functions outside of ready
validateNoAlphabetic = (input) => {
  // Regular expression to check if the input does not contain any alphabetic characters
  const regex = /^[^A-Za-z]*$/;

  // Test the input against the regex
  return regex.test(input); // Returns true if valid (no alphabetic characters), false otherwise
};
removeItem = (event, func) => {
  var buttonClicked = event.target;
  buttonClicked.parentElement.parentElement.remove();
  func();
};

updateAllTotal = (event, cost, cashTotalOfIte) => {
  quantityInputValue = event.target.value;
  updateTotalOfItem(quantityInputValue, cost, cashTotalOfIte);
};

updateTotalOfItem = (quant, cost, cashTotalOfIte) => {
  var total = quant * cost;
  cashTotalOfIte.innerHTML = "$" + total;
};

subTotal = (cashTotalOfIte) => {
  var subtotal = 0;
  for (var i = 0; i < removeCartButton.length; i++) {
    var cashTotalOfIte = cashTotalOfItem[i];
    subTotalTag = document.getElementById("TotalCart");
    subtotal =
      subtotal +
      Number(cashTotalOfIte.innerHTML.replace("$", "").replace(",", "."));
  }
  subTotalTag.innerHTML = "$" + subtotal;
  var total =
    subtotal + Number(shippingTag.innerHTML.replace(",", ".").replace("$", ""));
  totalCartTag.innerHTML = "$" + total;
};
