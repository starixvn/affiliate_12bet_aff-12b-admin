import React from 'react';

const ReportTitle = (props) => {
    return (
        <div className="report-title-container">
            <div className="report-title">{props.content}</div>
        </div>
    )
}

export default ReportTitle;