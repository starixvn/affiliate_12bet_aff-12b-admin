import React, { PureComponent, createRef } from 'react';
import PropTypes from 'prop-types';
import Option from './Option';

import { keys, visibleItemsInPanelTotal } from "../libs/constants";

export class SelectOptionList extends PureComponent {
    constructor(props) {
        super(props);

        const { dataSource, id, selectedKey } = props

        this.optionRefs = dataSource.map(() => {
            return createRef();
        })
        this.selectOptionsRef = createRef();

        const selectedIndex = dataSource.findIndex(i => i.key == selectedKey);

        this.state = {
            selectedIndex: selectedIndex < 0 ? 0 : selectedIndex,
            selectedOption: null,
            minLength: 0,
            maxLength: dataSource.length - 1,
        }

        this.optionId = `inputOption-${id}`;
        this.buttonId = `button-${id}`;
    }

    componentDidMount() {
        document.addEventListener("keydown", this.handleKeyPress);
        document.addEventListener("mouseover", this.handleMouseOver);

        const { selectedIndex } = this.state;
        const iniatialOption = this.optionRefs[selectedIndex];
        iniatialOption.current.classList.add("hover");

        const offsetHeight = iniatialOption.current.offsetHeight;
        this.selectOptionsRef.current.scrollTop = (selectedIndex - visibleItemsInPanelTotal) * offsetHeight;
        this.setState({ selectedOption: iniatialOption });
    }

    componentWillUnmount() {
        document.removeEventListener("keydown", this.handleKeyPress);
        document.removeEventListener("mouseover", this.handleMouseOver);
    }

    componentDidUpdate(prevProps) {
        const { dataSource } = this.props;

        if (!!dataSource.length && dataSource.length !== prevProps.dataSource.length) {
            this._resetSelectingOption();
            this.setState({ maxLength: dataSource.length - 1 });
        }
    }

    handleMouseOver = (e) => {
        if (this.selectOptionsRef.current.innerHTML.includes(e.target.outerHTML)) {
            const { selectedOption } = this.state;
            selectedOption && selectedOption.current && selectedOption.current.classList.remove("hover");

            const selectedIndex = this.optionRefs.findIndex(op => op.current.outerHTML.includes(e.target.outerHTML))

            if (selectedIndex !== -1) {
                const newOption = this.optionRefs[selectedIndex];
                newOption.current.classList.add("hover");

                this.setState({ selectedIndex, selectedOption: newOption });
            }
        }
    }

    _resetSelectingOption= () => {
        const { selectedOption } = this.state;
        selectedOption && selectedOption.current && selectedOption.current.classList.remove("hover");
        this.selectOptionsRef.current.scrollTop = 0;

        const iniatialOption = this.optionRefs[0];
        iniatialOption.current.classList.add("hover");

        this.setState({
            selectedIndex: 0,
            selectedOption: iniatialOption
        })
    }

    handleKeyPress = (e) => {
        if (e.target.id !== this.optionId && e.target.id !== this.buttonId) {
            return;
        }

        const keyCode = e.keyCode;
        const { selectedIndex, selectedOption, minLength, maxLength } = this.state;

        if (!selectedOption && !selectedIndex) {
            if (keyCode === keys.Down) {
                const newOption = this.optionRefs[0];
                newOption.current.classList.add("hover");

                this.setState({ selectedIndex: 0, selectedOption: newOption });

                return;
            }

            return;
        }

        //press enter
        if (keyCode === keys.Enter) {
            selectedOption.current.classList.remove("hover");
            const itemData = this.props.dataSource[selectedIndex];

            const newItemData = {
                key: itemData.key,
                value: itemData.value
            }

            this.props.onChange(newItemData);

            e.preventDefault();
            return;
        }

        const isKeyUp = keyCode === keys.Up;
        const isKeyDown = keyCode === keys.Down;

        if (isKeyUp || isKeyDown) {
            let newIndex = selectedIndex;

            //press up
            if (isKeyUp && selectedIndex > minLength) {
                newIndex = selectedIndex - 1;
            }

            //press down
            if (isKeyDown && selectedIndex < maxLength) {
                newIndex = selectedIndex + 1;
            }

            if (newIndex !== selectedIndex) {
                const offsetHeight = selectedOption.current.offsetHeight;
                selectedOption.current.classList.remove("hover");

                const newOption = this.optionRefs[newIndex];
                newOption.current.classList.add("hover");

                this.selectOptionsRef.current.scrollTop = (newIndex - visibleItemsInPanelTotal) * offsetHeight;
                this.setState({ selectedIndex: newIndex, selectedOption: newOption });
            }
        }
    }

    render() {
        const { dataSource, id } = this.props;

        return (
            <ul className="select-options" ref={this.selectOptionsRef}>
                {
                    dataSource.map((item, index) => {
                        return (
                            <Option
                                ref={this.optionRefs[index]}
                                id={`${id}-optionItem-${index}`}
                                key={`${id}optionItem${index}`}
                                itemData={item}
                                onChange={this.props.onChange} />
                        )
                    })
                }
            </ul>
        )
    }
}

SelectOptionList.propTypes = {
    selectedKey: PropTypes.oneOfType([PropTypes.number, PropTypes.string]).isRequired,
    dataSource: PropTypes.arrayOf(PropTypes.object).isRequired,
    id: PropTypes.string.isRequired,
    keyField: PropTypes.string,
    onChange: PropTypes.func
}

export default SelectOptionList;