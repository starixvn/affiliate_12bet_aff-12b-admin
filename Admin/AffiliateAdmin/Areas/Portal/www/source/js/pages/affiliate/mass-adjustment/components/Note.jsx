import React from 'react';

const Note = () => {
    const renderRow = () => {
        const rows = []

        for (let i = 0; i < 10; i++) {
            const index = i + 1;

            rows.push(
                <tr key={i}>
                    <td>{index}</td>
                    <td colSpan="3">Username{index}</td>
                    <td>{i + 11}</td>
                    <td>Reason{index}</td>
                </tr>
            )
        }

        return rows;
    };

    const sample = () => {
        return (
            <table className="data-table">
                <thead>
                    <th>Stt</th>
                    <th colSpan="3">User Name</th>
                    <th>Amount</th>
                    <th>Reason</th>
                </thead>
                <tbody>
                    {
                        renderRow()
                    }
                </tbody>
            </table>
        )
    };

    return (
        <div className="note-content">
            <p><strong>Note:</strong></p>
            <span> - For Import File Structure:</span>
            <ol>
                <li>Column A: Username</li>
                <li>Column B: Amount for Adjustment</li>
                <li>Column C: Reason for that Adjustment</li>
            </ol>
            <div className="sample-file">
                {
                    sample()
                }
                <p>For importing file template, please following this <a href="/Areas/Portal/www/sources/style/assets/template.xlsx" download>file</a>.</p>
            </div>
        </div>
    );
}

export default Note;