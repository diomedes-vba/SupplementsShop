document.addEventListener('DOMContentLoaded', () =>{
    
    console.log("ping");
    const digits = str => str.replace(/\D/g, '');
    
    // format CC number into groups of 4
    const formatCardNumber = val => (digits(val).match(/.{1,4}/g) || []).join(' ');
    
    // format MM/YY
    function formatExpiry(val) {
        let d = digits(val).slice(0,4);
        if (d.length >= 3) return d.slice(0,2) + '/' + d.slice(2);
        if (d.length >= 1 && d.length <=2) return d;
        return '';
    }
    
    const hookup = (selector, formatter, maxLen) => {
        const el = document.querySelector(selector);
        if (!el) return;
        el.setAttribute('inputmode', 'numeric');
        el.setAttribute('autocomplete',
            selector.includes('CardNumber') ? 'cc-number' :
                selector.includes('CVV') ? 'cc-csc' :
                    selector.includes('Expiration') ? 'cc-exp' : '');
        el.maxLength = maxLen;
        el.addEventListener('input', e => {
            const pos = e.target.selectionStart;
            e.target.value = formatter(e.target.value);
            e.target.setSelectionRange(pos, pos);
        });
    };
    
    hookup("[name='CardNumber']", formatCardNumber, 19);
    hookup("[name='ExpirationDate']", formatExpiry, 5);
    hookup("[name='CVV']", v => digits(v).slice(0,3), 3);
});