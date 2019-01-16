if [ $# -eq 0 ]
  then
    echo "No arguments supplied."
    echo "Retrieve URLs and check for changes on content that matches a list of regular expressions."
    echo "Usage: monitorandsaveversions.sh url regexes"
    echo "where url is an URL to monitor, and regexes is a file containing a list or regexes to use to filter the content before comparing."
    exit 1
fi

left=$(mktemp)
right=$(mktemp)
greppedleft=$(mktemp)
greppedright=$(mktemp)
url=$1
regexes=$2
# This deletes temp files on exit
trap 'rm -f "$left" "$right" "$greppedleft" "$greppedright"' EXIT
# Establish the "baseline":
wget -q -O "$left" "$url"
grep -f "$regexes" "$left" >"$greppedleft"
echo "Monitoring url $url"
echo "-using temp files $right $left $greppedright $greppedleft-"

# Okay, now check for updates forever:
while sleep 5; do
    echo "$(date +%Y%m%d%H%M%S) - reading $url"
    wget -q -O "$right" "$url"
    grep -f "$regexes" "$right" >"$greppedright"
    if ! diff "$greppedleft" "$greppedright" >/dev/null; then
        version=$(echo $url | sed -e 's/\///g' | sed -e 's/://g' | sed -e 's/\.//g')-$(date +%Y%m%d%H%M%S)
        echo "$(date +%Y%m%d%H%M%S) - Changes detected in '$url', saving to file $version"
        cp "$right" "$version"
        cp "$right" "$left"
        grep -f "$regexes" "$left" >"$greppedleft"
    fi
done

