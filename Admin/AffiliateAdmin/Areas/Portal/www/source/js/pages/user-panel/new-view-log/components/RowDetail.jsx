import React from 'react';

export const RowDetail = () => {
    const renderHeader = () => {
        const columns = []

        for (let i = 1; i < 9; i++) {
            columns.push(
                <td key={i} className="header-column"></td>
            )
        }

        return columns;
    };

    const renderRow = () => {
        const rows = []

        for (let i = 1; i < 4; i++) {
            rows.push(
                <tr key={i}>
                    <td className="key">ABC</td>
                    <td className="text-center">123</td>
                    <td className="key">ABC</td>
                    <td className="text-center">123</td>
                    <td className="key">ABC</td>
                    <td className="text-center">123</td>
                    <td className="key">ABC</td>
                    <td className="text-center">This member does not exist in Affiliate System!</td>
                </tr>
            )
        }

        return rows;
    };

    return (
        <table className="row-detail-table">
            <thead>
                <tr>
                    {
                        renderHeader()
                    }
                </tr>
            </thead>
            <tbody>
                {
                    renderRow()
                }
            </tbody>
        </table>
    );
}

export default RowDetail;