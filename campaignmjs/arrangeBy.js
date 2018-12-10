function arrangeBy(keyName) {
    return function(input) {
        var result = {};

        input.forEach(function(data, index){
            if (keyName in data)
            {
                var keyValue = data[keyName];
                if (keyValue != null)
                {
                    if (keyValue in result)
                    {
                        result[keyValue].push(data);
                    }
                    else
                    {
                        result[keyValue] = [data];
                    }
                }
            }
        });

        return result;
    }
}

module.exports = arrangeBy;