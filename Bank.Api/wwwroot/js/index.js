(function () {
    const onDepositClick = (event) => {
        let value = Number(event.target.previousElementSibling.value);
        deposit({ accountNo: 1001, amount: value })
    }

    const onWithdrawClick = (event) => {
        let value = Number(event.target.previousElementSibling.value);
        withdraw({ accountNo: 1001, amount: value })
    }

    const onPaymentClick = (event) => {
        let inputedDuedate = document.querySelector('#pay > #duedate');
        let inputedNumber = document.querySelector('#pay > #number');
        let inputedAmount = document.querySelector('#pay > #amount');
        payment({
            accountNo: 1001,
            invoice: {
                amount: inputedAmount.value, duedate: inputedDuedate.value, number: inputedNumber.value
            }
        });
    }

    const withdraw = (command) => {        
        fetch('/api/account/withdraw', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })            
            .then(loadStatement(command.accountNo))
            
    }

    const deposit = (command) => {
        fetch('/api/account/deposit', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })
        .then(data => console.log(data));
    }

    const payment = (command) => {
        fetch('/api/account/payment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })
        .catch(error => console.log(error));
    }

    const loadStatement = (accountNo) => {
        fetch(`/api/account/${accountNo}/statement`)
            .then(response => response.json())
            .then(fillTable);
    }

    const fillTable = (data) => {
        const tbody = document.querySelector('table > tbody');

        if (data.statements.length === 0)
            tbody.append(addEmpty());

        data.statements.forEach(statement => {
            let clone = addRow(statement);
            tbody.append(clone);
        });
    }

    const addRow = (statement) => {
        const template = document.getElementById('tbl-row');
        const clone = document.importNode(template.content, true);
        const td = clone.querySelectorAll('td');
        td[0].innerText = statement.date;
        td[1].innerText = statement.operation;
        td[2].innerText = statement.amount;
        return clone;
    }

    const addEmpty = () => {
        const template = document.getElementById('empty-row');
        const clone = document.importNode(template.content, true);
        return clone;
    }

    const showToast = () => {
        const toasterEl = document.querySelector('.toast')

        const toaster = new bootstrap.Toast(toasterEl)
        toaster.show();
    }
    


    document.getElementById('btn-deposit').addEventListener('click', onDepositClick)
    document.getElementById('btn-withdraw').addEventListener('click', onWithdrawClick)
    document.getElementById('btn-pay').addEventListener('click', onPaymentClick)
    

    loadStatement(1001);
    showToast();
})();