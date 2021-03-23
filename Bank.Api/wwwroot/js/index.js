(function () {
    const onDepositClick = (event) => {
        let deposit = Number(event.target.previousElementSibling.value);
        withdraw({ accountNo: 1001, amount: deposit })
    }

    const withdraw = (command) => {        
        fetch('/api/account/deposit', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(command)
        })
        .then(data => console.log(data));
    }

    const loadStatement = (accountNo) => {
        fetch(`/api/account/${accountNo}/statement`)
            .then(response => response.json())
            .then(fillTable);
    }

    const fillTable = (data) => {
        const tbody = document.querySelector('table > tbody');
        for (var statement in data.statements) {
            let clone = addRow(statement);
            tbody.append(clone);
        }
    }

    const addRow = (statement) => {
        const template = document.getElementById('tbl-row');
        const clone = document.importNode(template.content, true);
        const td = clone.querySelectorAll('td');
        td[0] = statement.date;
        td[1] = statement.operation;
        td[2] = statement.amount;
        return clone;
    }


    document.getElementById('btn-deposit').addEventListener('click', onDepositClick)

    loadStatement(1001);
})();