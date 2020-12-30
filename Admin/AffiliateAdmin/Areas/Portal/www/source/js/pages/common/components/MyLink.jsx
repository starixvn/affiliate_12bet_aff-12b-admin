import React from 'react';
import PropTypes from 'prop-types';

export const MyLink = (props) => {
    const { id, className, children, title } = props

    const handleClick = (event) => {
        event.preventDefault();

        props.onClick();
    }

    return (
        <a href="#" id={id} className={className} title={title} onClick={(e) => handleClick(e)}>
            {children}
        </a>
    )
}

MyLink.propTypes = {
    onClick: PropTypes.func.isRequired,
    id: PropTypes.string,
    className: PropTypes.string,
    children: PropTypes.any,
    title: PropTypes.string
};

MyLink.defaultProps = {
    id: null,
    className: null,
    children: null,
    title: null
};

export default React.memo(MyLink);