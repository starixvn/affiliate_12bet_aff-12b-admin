import React from 'react';
import { resolveClientUrl } from '../../../../modules/utils';

export const Note = () => {
    const renderRow = () => {
        const rows = []

        for (let i = 1; i < 4; i++) {
            const index = i + 1;

            rows.push(
                <tr key={i}>
                    <td className="text-center">{index}</td>
                    <td colSpan="2">12BETAFF00{i}</td>
                    <td className="text-right pr-1">6.40</td>
                    <td>Comment adjustment</td>
                </tr>
            )
        }

        return rows;
    };

    const sample = () => {
        return (
            <table className="data-table sample-table">
                <thead>
                    <th></th>
                    <th colSpan="2">A</th>
                    <th>B</th>
                    <th>C</th>
                </thead>
                <tbody>
                    <tr>
                        <td className="text-center">1</td>
                        <td className="text-center font-weight-bold" colSpan="2">Username</td>
                        <td className="text-center font-weight-bold">Amount</td>
                        <td className="text-center font-weight-bold">Reason</td>
                    </tr>
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
                <p>For importing file template, please following this <a href={resolveClientUrl("areas/portal/www/dist/assets/template.xlsx")} download>file</a>.</p>
            </div>
        </div>
    );
}

export default Note;