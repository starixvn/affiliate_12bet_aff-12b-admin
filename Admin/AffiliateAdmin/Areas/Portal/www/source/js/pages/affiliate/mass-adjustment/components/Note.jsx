import React from 'react';

const Note = () => {
    const renderRow = () => {
        const rows = []

        for (let i = 0; i < 8; i++) {
            const index = i + 1;

            rows.push(
                <tr key={i}>
                    <td className="text-center">{index}</td>
                    <td colSpan="2">Sample{index}</td>
                    <td>6.4</td>
                    <td>test</td>
                </tr>
            )
        }

        return rows;
    };

    const sample = () => {
        return (
            <table className="data-table">
                <thead>
                    <th></th>
                    <th colSpan="2">A</th>
                    <th>B</th>
                    <th>C</th>
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