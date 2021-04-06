import React, { Fragment } from 'react';

export const renderHeader = () => {
    return (
        <thead key="thead">
            <tr>
                <th rowSpan="2" colSpan="4">Function</th>
                <th colSpan="2">Individual Permission</th>
                <th colSpan="2">Role Permision</th>
            </tr>
            <tr>
                <th>Read</th>
                <th>Host</th>
                <th>Read</th>
                <th>Host</th>
            </tr>
        </thead>
    );
}

export const renderColumns = (dataItem) => {

    return (
        <Fragment>
            <td className="text-center">
                {
                    dataItem.IsRead && <span className="oi oi-check text-primary"></span>
                }
            </td>

            <td className="text-center">
                {
                    dataItem.IsHost && <span className="oi oi-check text-primary"></span>
                }
            </td>
            <td className="text-center">
                {
                    dataItem.IsRoleRead && <span className="oi oi-check text-primary"></span>
                }
            </td>

            <td className="text-center">
                {
                    dataItem.IsRoleHost && <span className="oi oi-check text-primary"></span>
                }
            </td>
        </Fragment>
    );
}