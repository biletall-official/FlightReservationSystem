document.addEventListener("DOMContentLoaded", function () {

    neredenInput.addEventListener("input", function () {
        getSuggestions(this.value, 'nereden');
    });

    nereyeInput.addEventListener("input", function () {
        getSuggestions(this.value, 'nereye');
    });
});

function showSuggestions(suggestions, type) {
    const input = document.getElementById(type);
    const list = document.createElement('ul');
    list.classList.add('suggestions');

    suggestions.forEach(item => {
        const listItem = document.createElement('li');
        listItem.textContent = item;
        listItem.addEventListener('click', () => {
            input.value = item;
            list.innerHTML = ''; 
        });
        list.appendChild(listItem);
    });
    
    const previousList = input.nextElementSibling;
    if (previousList && previousList.tagName === 'UL') {
        previousList.remove();
    }
    input.insertAdjacentElement('afterend', list);
}
