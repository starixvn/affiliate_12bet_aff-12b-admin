import React from 'react';
import { resolveClientUrl } from '../../../../modules/utils';

const Note = () => {
    const href = resolveClientUrl('Areas/Portal/www/sources/style/assets/template.xlsx');

    const renderRow = () => {
        let rows = []

        for (let i = 0; i < 9; i++) {
            rows.push(
                <tr key={i}>
                    <td>{i + 1}</td>
                    <td colSpan="3">Username01</td>
                    <td>{i + 10}</td>
                    <td>Reason01</td>
                </tr>
            )
        }

        return rows;
    };

    const sample = () => {
        return (
            <table className="sample-table">
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