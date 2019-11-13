import React from 'react';
import { Row } from 'reactstrap';

export const Panel = (props) => {
    const { heading, children, className } = props;

    return (
        <div className="home-panel">
            <Row>
                <span className={className}></span>
                <h5>{heading}</h5>
            </Row>
            {children}
        </div>
    )
}

export default React.memo(Panel);