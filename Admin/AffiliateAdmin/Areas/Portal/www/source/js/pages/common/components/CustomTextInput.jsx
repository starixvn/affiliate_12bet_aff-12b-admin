import React, { PureComponent } from 'react';

export default class CustomTextInput extends PureComponent {
    constructor(props) {
        super(props);

        this.textInput = React.createRef();
    }

    focusTextInput = () => {
        this.textInput.current.focus();
    }

    render() {
        const { name, value, onChange } = this.props;
        return (
            <input className="form-control" name={name} value={value} onChange={onChange} ref={this.textInput} />
        )
    }
}