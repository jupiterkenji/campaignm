function arrangeBy(keyName) {
    return function(input) {
        var result = {};

        input.forEach(function(data, index){
            var keyValue = data[keyName];
            if (keyValue in result)
            {
                result[keyValue].push(data);
            }
            else
            {
                result[keyValue] = [data];
            }
        });

        return result;
    }
}

module.exports = arrangeBy;