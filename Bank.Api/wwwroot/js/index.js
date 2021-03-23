(function () {
    const onDepositClick = (event) => {
        let value = Number(event.target.previousElementSibling.value);
        deposit({ accountNo: 1001, amount: value })
    }

    const onWithdrawClick = (event) => {
        let value = Number(event.target.previousElementSibling.value);
        withdraw({ accountNo: 1001, amount: value })
    }

    const onPaymentClick = () => {
        let inputedDuedate = document.querySelector('#pay input#duedate');
        let inputedNumber = document.querySelector('#pay input#number');
        let inputedAmount = document.querySelector('#pay input#amount');
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
            .then(response => reloadStatement(response, command.accountNo))
            .catch(showAlert);
    }

    const deposit = (command) => {
        fetch('/api/account/deposit', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })
            .then(response => reloadStatement(response, command.accountNo))
            .catch(showAlert);
    }

    const payment = (command) => {
        fetch('/api/account/payment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })
            .then(response => reloadStatement(response, command.accountNo))
            .catch(showAlert);
    }

    const loadStatement = (accountNo) => {
        fetch(`/api/account/${accountNo}/statement`)
            .then(response => response.json())
            .then(fillTable);
    }

    const reloadStatement = (response, accountNo) => {
        if (response.ok) {
            hideMessage();
            loadStatement(accountNo)
        }
        else
            throw response;
    }


    const fillTable = (data) => {

        document.querySelector('#owner').innerText = data.title;
        document.querySelector('#balance').innerText = formatCurrency(data.balance);

        const tbody = cleanTable();

        if (data.statements.length === 0)
            tbody.append(addEmpty());

        data.statements.forEach(statement => {
            let clone = addRow(statement);
            tbody.append(clone);
        });
    }

    const cleanTable = () => {
        const table = document.querySelector('table');
        table.tBodies[0].remove();
        return table.createTBody();
    }

    const addRow = (statement) => {
        const template = document.getElementById('tbl-row');
        const clone = document.importNode(template.content, true);
        const td = clone.querySelectorAll('td');
        td[0].innerText = statement.date;
        td[1].innerText = statement.operation;
        td[2].innerText = formatCurrency(statement.amount);
        return clone;
    }

    const addEmpty = () => {
        const template = document.getElementById('empty-row');
        const clone = document.importNode(template.content, true);
        return clone;
    }

    const showAlert = (message) => {
        const alert = document.getElementById('alert-msg');
        alert.innerHTML = 'Ocorreu um erro no processamento, reveja os dados'
        alert.style.display = 'block';
    }   

    const hideMessage = () => {
        const alert = document.getElementById('alert-msg');
        alert.style.display = 'none';
    }

    const formatCurrency = (value) => 
        new Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(value);

    document.getElementById('btn-deposit').addEventListener('click', onDepositClick)
    document.getElementById('btn-withdraw').addEventListener('click', onWithdrawClick)
    document.getElementById('btn-pay').addEventListener('click', onPaymentClick)
    
    loadStatement(1001);
    
})();