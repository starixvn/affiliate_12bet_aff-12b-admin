import React, { Fragment } from 'react';

export const RowDetail = (props) => {
    const { oldData, newData } = props;
    const oldDataObject = oldData ? JSON.parse(oldData).root.row : {};
    const newDataObject = newData ? JSON.parse(newData).root.row : {};
    const oldDataList = Object.keys(oldDataObject);
    const newDataList = Object.keys(newDataObject);
    const length = newDataList.length || oldDataList.length;
    const rowNumber = length / 4;

    const renderHeader = () => {
        const columns = [];

        for (let i = 1; i < 9; i++) {
            columns.push(
                <td key={i} className="header-column"></td>
            )
        }

        return columns;
    };

    const renderColumn = (oldValue, newValue) => {
        if (!oldValue && !newValue) {
            return '';
        }

        if (oldValue && newValue) {
            return oldValue == newValue ? (
                oldValue
            ) : (
                    <Fragment>
                        <p className="text-primary">{newValue}</p>
                        <span className="text-secondary">{oldValue}</span>
                    </Fragment>
                )
        }


        return oldValue || newValue;
    }

    const renderRow = () => {
        const rows = []

        for (let i = 0; i < rowNumber; i++) {
            const keyColumn1 = oldDataList[i * 4] || '';
            const keyColumn2 = oldDataList[i * 4 + 1] || '';
            const keyColumn3 = oldDataList[i * 4 + 2] || '';
            const keyColumn4 = oldDataList[i * 4 + 3] || '';

            rows.push(
                <tr key={i}>
                    <td className="key">{keyColumn1.slice(1)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn1], newDataObject[keyColumn1])}</td>
                    <td className="key">{keyColumn2.slice(1)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn2], newDataObject[keyColumn2])}</td>
                    <td className="key">{keyColumn3.slice(1)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn3], newDataObject[keyColumn3])}</td>
                    <td className="key">{keyColumn4.slice(1)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn4], newDataObject[keyColumn4])}</td>
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