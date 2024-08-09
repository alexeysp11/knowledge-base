function roundWithPrecision(value, precision) {
    let coef = Math.pow(10, precision); 
    return (Math.round(value * coef) / coef).toFixed(precision); 
}

function calculateTax(value, taxPercent) {
    let beforeTax = roundWithPrecision(Number(value), 2); 
    let taxAmount = roundWithPrecision(Number(beforeTax) * (Number(taxPercent) / 100), 2); 
    return {
        "beforeTax": beforeTax, 
        "taxAmount": taxAmount
    }; 
}

function addTax(value, taxPercent) {
    let tax = calculateTax(Number(value), Number(taxPercent)); 
    let total = roundWithPrecision(Number(tax.beforeTax) + Number(tax.taxAmount), 2); 
    return {
        "beforeTax": tax.beforeTax, 
        "taxAmount": tax.taxAmount, 
        "total": total
    }; 
}

function substractTax(value, taxPercent) {
    let tax = calculateTax(Number(value), Number(taxPercent)); 
    let total = roundWithPrecision(Number(tax.beforeTax) - Number(tax.taxAmount), 2); 
    return {
        "beforeTax": tax.beforeTax, 
        "taxAmount": tax.taxAmount, 
        "total": total
    }; 
}
