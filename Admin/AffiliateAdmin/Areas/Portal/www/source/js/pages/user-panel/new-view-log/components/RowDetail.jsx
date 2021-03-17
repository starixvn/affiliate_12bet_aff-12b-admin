import React, { Fragment } from 'react';
import { handleParseToObject, removeFirstCharacter } from '../libs/utils';

export const RowDetail = (props) => {
    const { oldData, newData } = props;
    const oldDataObject = handleParseToObject(oldData);
    const newDataObject = handleParseToObject(newData);

    const oldDataList = Object.keys(oldDataObject);
    const newDataList = Object.keys(newDataObject);
    const length = oldDataList.length > newDataList.length ? oldDataList.length : newDataList.length;
    const rowNumber = length / 4;

    const renderColumn = (oldValue, newValue) => {
        if (!oldValue && !newValue) {
            return '';
        }

        return oldValue == newValue ? (
            `${oldValue}`
        ) : (
                <Fragment>
                    {
                        !!newValue && <p className="text-primary mb-0">{`${newValue}`}</p>
                    }
                    {
                        !!oldValue && <span className="text-secondary">{`${oldValue}`}</span>
                    }
                </Fragment>
            )
    }

    const renderRow = () => {
        const rows = []

        for (let i = 0; i < rowNumber; i++) {
            const keyColumn1 = oldDataList[i * 4] || newDataList[i * 4];
            const keyColumn2 = oldDataList[i * 4 + 1] || newDataList[i * 4 + 1] || '';
            const keyColumn3 = oldDataList[i * 4 + 2] || newDataList[i * 4 + 2] || '';
            const keyColumn4 = oldDataList[i * 4 + 3] || newDataList[i * 4 + 3] || '';

            rows.push(
                <tr key={i}>
                    <td className="key">{removeFirstCharacter(keyColumn1)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn1], newDataObject[keyColumn1])}</td>
                    <td className="key">{removeFirstCharacter(keyColumn2)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn2], newDataObject[keyColumn2])}</td>
                    <td className="key">{removeFirstCharacter(keyColumn3)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn3], newDataObject[keyColumn3])}</td>
                    <td className="key">{removeFirstCharacter(keyColumn4)}</td>
                    <td className="text-center">{renderColumn(oldDataObject[keyColumn4], newDataObject[keyColumn4])}</td>
                </tr>
            )
        }

        return rows;
    };

    return (
        <table className="row-detail-table">
            <tbody>
                {
                    renderRow()
                }
            </tbody>
        </table>
    );
}

export default RowDetail;