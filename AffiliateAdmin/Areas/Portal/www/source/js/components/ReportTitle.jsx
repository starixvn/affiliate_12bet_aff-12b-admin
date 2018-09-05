import React from 'react';

const ReportTitle = (props) => {
    return (
        <div className="report-title-container">
            <span className="report-title">{props.content}</span>
        </div>
    )
}

export default ReportTitle;