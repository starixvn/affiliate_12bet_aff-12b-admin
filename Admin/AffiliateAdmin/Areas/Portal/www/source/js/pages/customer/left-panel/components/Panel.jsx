import React from 'react';
import { Button } from 'reactstrap';

const Panel = (props) => {
    const { heading, children, style, ...attributes } = props;

    return (
        <div className="left-submenu" {...attributes}>
            <ul>
                <li>
                    <Button color="link hide-link">
                        {heading}
                    </Button>
                </li>
            </ul>
            <div className="content" style={{ ...style }}>
                {children}
            </div>
        </div>
    )
}

export default React.memo(Panel);