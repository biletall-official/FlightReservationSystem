document.addEventListener('DOMContentLoaded', function () {
    const toggleButton = document.getElementById('toggle-passenger-controls');
    const controls = document.getElementById('passenger-controls');
    let isVisible = false;

    // Yolcu Kontrollerini Göster/Gizle
    toggleButton.addEventListener('click', function () {
        isVisible = !isVisible;
        controls.style.display = isVisible ? 'block' : 'none';
    });

    const quantityButtons = document.querySelectorAll('.quantity-btn');

    // Yolcu Sayısını Artırma/Azaltma İşlemleri
    quantityButtons.forEach(button => {
        button.addEventListener('click', function () {
            const action = this.dataset.action;
            const targetId = this.dataset.target;
            const targetElement = document.getElementById(targetId);
            let value = parseInt(targetElement.textContent);

            if (action === 'increase') {
                if (targetId === 'Bebek') {
                    if (value < 4 && validateBebekIncrease()) {
                        value++;
                    } else {
                        alert('Her bebek için en az bir yetişkin veya 65 yaş üzeri yolcu olmalıdır.');
                        return;
                    }
                } else if (validatePassengerCountsBeforeIncrease()) {
                    value++;
                }
            } else if (action === 'decrease') {
                if (targetId === 'Bebek' && value > 0) {
                    value--;
                } else if (value > 0) {
                    value--;
                }
                return;
            }

            targetElement.textContent = value;
            validatePassengerCounts();
        });
    });

    // Yolcu Sayısı Validasyonları
    function validatePassengerCounts() {
        const yetiskin = parseInt(document.getElementById('Yetiskin').textContent);
        const cocuk = parseInt(document.getElementById('Cocuk').textContent);
        const ogrenci = parseInt(document.getElementById('Ogrenci').textContent);
        const yasUzeri = parseInt(document.getElementById('65YasUzeri').textContent);
        const bebek = parseInt(document.getElementById('Bebek').textContent);
        
        const totalPassengers = yetiskin + cocuk + ogrenci + yasUzeri + bebek;
        const totalAdults = yetiskin + yasUzeri + ogrenci;

        // Yetişkin, Çocuk, Öğrenci ve 65 Yaş Üzeri toplamı 4 veya daha az olmalı
        if (totalAdults > 4) {
            alert('Yetişkin, Çocuk, Öğrenci ve 65 Yaş Üzeri yolcuların toplamı 4 veya daha az olmalıdır.');
        }

        // En az 1 yetişkin veya 65 yaş üstü yolcu olmalı
        if (totalAdults < bebek) {
            alert('En az 1 yetişkin veya 65 yaş üstü yolcu olmalıdır.');
        }

        // Bebek sayısı 0 ile 4 arasında olmalı
        if (bebek < 0 || bebek > 4) {
            alert('Bebek sayısı 0 ile 4 arasında olmalıdır.');
        }

        // Toplam yolcu sayısı 8 veya daha az olmalı
        if (totalPassengers > 8) {
            alert('Toplam yolcu sayısı 8 veya daha az olmalıdır.');
        }
        return;
    }

    // Bebek artışını kontrol eden fonksiyon
    function validateBebekIncrease() {
        const yetiskin = parseInt(document.getElementById('Yetiskin').textContent);
        const yasUzeri = parseInt(document.getElementById('65YasUzeri').textContent);
        return yetiskin + yasUzeri >= parseInt(document.getElementById('Bebek').textContent) + 1;
    }

    // Artış yapmadan önce yolcu sayılarını kontrol eden fonksiyon
    function validatePassengerCountsBeforeIncrease() {
        const yetiskin = parseInt(document.getElementById('Yetiskin').textContent);
        const cocuk = parseInt(document.getElementById('Cocuk').textContent);
        const ogrenci = parseInt(document.getElementById('Ogrenci').textContent);
        const yasUzeri = parseInt(document.getElementById('65YasUzeri').textContent);

        const totalAdults = yetiskin + yasUzeri + ogrenci + cocuk;
        return totalAdults <= 4 || (totalAdults < 8);
    }
});
